using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Blog : Entity<Guid>
    {
        public Guid AuthorId { get; set; }
        public string CoverImageURL { get; set; }
        public bool IsPublic { get; set; }

        public Author Author { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
