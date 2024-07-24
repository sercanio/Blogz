using Microsoft.AspNetCore.Identity;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Auth.Commands.Register;

public class RegisteredResponse : IResponse
{
    public string Id { get; set; }
    public IdentityResult IdentityResult { get; set; }

    public RegisteredResponse()
    {
        IdentityResult = null!;
        Id = string.Empty;
    }

    public RegisteredResponse(IdentityResult identityResult)
    {
        IdentityResult = identityResult;
        Id = string.Empty;
    }
}
