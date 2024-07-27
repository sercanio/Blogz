using BlogZ.Persistence.Contexts;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Repositories.Abstractions;

namespace Persistence.Repositories;

public class CommentRepository : EfRepositoryBase<Comment, Guid, ApplicationDbContext>, ICommentRepository
{
    private readonly ApplicationDbContext _context;
    public CommentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}