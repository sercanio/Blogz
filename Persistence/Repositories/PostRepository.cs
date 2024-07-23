using BlogZ.Persistence.Contexts;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Repositories.Abstractions;

namespace Persistence.Repositories;

public class PostRepository : EfRepositoryBase<Post, Guid, ApplicationDbContext>, IPostRepository
{
    private readonly ApplicationDbContext _context;
    public PostRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}