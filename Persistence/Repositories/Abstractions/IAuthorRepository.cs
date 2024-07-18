using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Persistence.Repositories.Abstractions;

public interface IAuthorRepository : IAsyncRepository<Author, Guid>, IRepository<Author, Guid>
{
}