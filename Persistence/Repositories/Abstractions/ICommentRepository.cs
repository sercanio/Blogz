using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Persistence.Repositories.Abstractions;

public interface ICommentRepository : IAsyncRepository<Comment, Guid>, IRepository<Comment, Guid>
{
}
