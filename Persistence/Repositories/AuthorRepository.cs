using BlogZ.Persistence.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Repositories.Abstractions;

namespace Persistence.Repositories;

public class AuthorRepository : EfRepositoryBase<Author, Guid, ApplicationDbContext>, IAuthorRepository
{
    private readonly ApplicationDbContext _context;

    public AuthorRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Author>> GetListByDynamicAsync(DynamicQuery query, int index, int size)
    {
        var authorsQuery = _context.Authors
            .Include(a => a.User)
            .AsQueryable();

        if (query.Filter != null)
        {
            if (query.Filter.Field == "userName" && query.Filter.Operator == "contains")
            {
                authorsQuery = authorsQuery.Where(a => a.User.UserName.Contains(query.Filter.Value));
            }
        }

        var authors = await authorsQuery
            .Skip(index * size)
            .Take(size)
            .ToListAsync();

        return authors;
    }
}
