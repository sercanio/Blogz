using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands.Logout;

public class LogoutCommand : IRequest<LogoutResponse>
{
    public string ReturnUrl { get; set; }

    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, LogoutResponse>
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutCommandHandler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<LogoutResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();

            return new LogoutResponse { Succeeded = true, ReturnUrl = request.ReturnUrl };
        }
    }
}