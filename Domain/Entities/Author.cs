using Microsoft.AspNetCore.Identity;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Author : Entity<Guid>
{
    public string UserId { get; set; }
    public string? ProfileImageURL { get; set; }
    public string? Biography { get; set; }

    public ICollection<Comment> Comments { get; set; }
    public IdentityUser User { get; set; }
}
