using BlogZ.Persistence.Contexts;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Repositories.Abstractions;

namespace Persistence.Repositories;

public class BlogRepository : EfRepositoryBase<Blog, Guid, ApplicationDbContext>, IBlogRepository
{
    private readonly ApplicationDbContext _context;
    public BlogRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}