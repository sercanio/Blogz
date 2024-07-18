using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Tag : Entity<Guid>
{
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public string Description { get; set; }
}
