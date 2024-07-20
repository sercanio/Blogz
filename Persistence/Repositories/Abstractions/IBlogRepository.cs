using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Persistence.Repositories.Abstractions;

public interface IBlogRepository : IAsyncRepository<Blog, Guid>, IRepository<Blog, Guid>
{
}