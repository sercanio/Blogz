using BlogZ.Persistence.Contexts;
using Domain.Entities;
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
}