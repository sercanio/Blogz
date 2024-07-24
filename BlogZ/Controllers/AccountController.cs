using Application.Features.Auth.Commands.Register;
using Blogz.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogz.Controllers;

public class AccountController : Controller
{
    private readonly IMediator _mediator;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(IMediator mediator, SignInManager<IdentityUser> signinManager, UserManager<IdentityUser> userManager)
    {
        _mediator = mediator;
        _signInManager = signinManager;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl = null)
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }

        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        LoginViewModel model = new LoginViewModel
        {
            ReturnUrl = returnUrl ?? Url.Content("~/"),
            ExternalLogins = externalLogins
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        model.ReturnUrl = returnUrl;

        model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        if (model == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register(string returnUrl = null)
    {
        //var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        List<AuthenticationScheme>? externalLogins = null;

        RegisterViewModel model = new()
        {
            ExternalLogins = externalLogins ?? new List<AuthenticationScheme>(),
            ReturnUrl = returnUrl ?? ""
        };

        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl, CancellationToken cancellationToken)
    {
        RegisterCommand command = new() { Email = model.Email, Password = model.Password, UserName = model.UserName };

        RegisteredResponse response = await _mediator.Send(command);

        if (response.IdentityResult.Succeeded)
        {
            var loginViewModel = new LoginViewModel()
            {
                Email = model.Email,
                Password = model.Password,
                Succeeded = true
            };
            return View("Login", loginViewModel);
        }
        model.Errors = response.IdentityResult.Errors;

        return View("Register", model);
    }

    private IdentityUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<IdentityUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        }
    }

    private IUserEmailStore<IdentityUser> GetEmailStore(IUserStore<IdentityUser> userStore)
    {
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }
        return (IUserEmailStore<IdentityUser>)userStore;
    }

    [HttpGet]
    public IActionResult Lockout()
    {
        return View();
    }

    [HttpGet]
    public IActionResult LoginWith2fa(bool rememberMe, string returnUrl = null)
    {
        // Implement 2FA login logic here
        return View();
    }
}
