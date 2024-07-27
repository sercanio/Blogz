using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Post : Entity<Guid>
{
    public Guid BlogId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Slug { get; set; }
    public bool IsPublic { get; set; }
    public string? CoverImageURL { get; set; }

    public Blog Blog { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Tag> Tags { get; set; }
}
