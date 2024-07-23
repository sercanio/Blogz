using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Persistence.Repositories.Abstractions;

public interface IPostRepository : IAsyncRepository<Post, Guid>, IRepository<Post, Guid>
{
}
