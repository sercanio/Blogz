using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Logout;
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

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl = null)
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }

        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        // Assume _signInManager.GetExternalAuthenticationSchemesAsync() is handled elsewhere or removed
        List<AuthenticationScheme>? externalLogins = null;

        LoginViewModel model = new LoginViewModel
        {
            ReturnUrl = returnUrl ?? Url.Content("~/"),
            ExternalLogins = externalLogins ?? new List<AuthenticationScheme>()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        model.ReturnUrl = returnUrl;

        if (model == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        if (ModelState.IsValid)
        {
            LoginCommand command = new()
            {
                Email = model.Email,
                Password = model.Password,
                RememberMe = model.RememberMe
            };

            LoggedResponse response = await _mediator.Send(command);

            if (response.IdentityResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            if (response.IdentityResult.RequiresTwoFactor)
            {
                return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
            }
            if (response.IdentityResult.IsLockedOut)
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
    public async Task<IActionResult> Logout(string returnUrl = null)
    {
        LogoutCommand command = new() { ReturnUrl = returnUrl ?? Url.Content("~/") };
        LogoutResponse response = await _mediator.Send(command);

        if (response.Succeeded)
        {
            return LocalRedirect(response.ReturnUrl);
        }

        // Handle failure case if necessary
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register(string returnUrl = null)
    {
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
    public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null, CancellationToken cancellationToken = default)
    {
        RegisterCommand command = new() { Email = model.Email, Password = model.Password, UserName = model.UserName };

        RegisteredResponse response = await _mediator.Send(command);

        if (response.IdentityResult.Succeeded)
        {
            returnUrl ??= Url.Content("~/");
            var loginViewModel = new LoginViewModel()
            {
                Email = model.Email,
                Password = model.Password,
                ReturnUrl = returnUrl
            };
            return View("Login", loginViewModel);
        }
        model.Errors = response.IdentityResult.Errors;
        return View("Register", model);
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
