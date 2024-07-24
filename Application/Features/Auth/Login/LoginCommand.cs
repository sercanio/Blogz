using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<LoggedResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedResponse>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public LoginCommandHandler(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<LoggedResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            IdentityUser userInDB = await _userManager.FindByEmailAsync(request.Email);

            if (userInDB == null)
            {
                return new LoggedResponse { IdentityResult = SignInResult.Failed };
            }

            var result = await _signInManager.PasswordSignInAsync(userInDB.UserName, request.Password, request.RememberMe, lockoutOnFailure: false);

            return new LoggedResponse { IdentityResult = result };
        }
    }
}