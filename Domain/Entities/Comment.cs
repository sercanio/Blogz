using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Comment : Entity<Guid>
{
    public Guid AuthorId { get; set; }
    public Guid PostId { get; set; }
    public string Content { get; set; }

    public Author Author { get; set; }
    public Post Post { get; set; }
}
