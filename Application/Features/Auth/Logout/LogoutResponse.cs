using NArchitecture.Core.Application.Responses;

namespace Application.Features.Auth.Commands.Logout;

public class LogoutResponse : IResponse
{
    public bool Succeeded { get; set; }
    public string ReturnUrl { get; set; }
}
