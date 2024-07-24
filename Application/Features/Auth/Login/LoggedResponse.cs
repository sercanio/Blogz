using Microsoft.AspNetCore.Identity;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Auth.Commands.Login;

public class LoggedResponse : IResponse
{
    public SignInResult IdentityResult { get; set; }
}
