using Microsoft.AspNetCore.Identity;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Authors.Commands.Create;

public class CreatedAuthorResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Biography { get; set; }
    public string? ProfilePictureUrl { get; set; }

    public IdentityUser User { get; set; }
}