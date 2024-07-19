using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Blog : Entity<Guid>
    {
        public Guid AuthorId { get; set; }

        public Author Author { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
