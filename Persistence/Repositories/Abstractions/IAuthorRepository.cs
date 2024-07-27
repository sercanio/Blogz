using Domain.Entities;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Persistence.Repositories;

namespace Persistence.Repositories.Abstractions;

public interface IAuthorRepository : IAsyncRepository<Author, Guid>, IRepository<Author, Guid>
{
    Task<List<Author>> GetListByDynamicAsync(DynamicQuery query, int index, int size);
}
