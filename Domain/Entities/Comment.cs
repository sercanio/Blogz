using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Comment : Entity<Guid>
{
    public Guid AuthorId { get; set; }
    public string Content { get; set; }

    public Author Author { get; set; }
}
