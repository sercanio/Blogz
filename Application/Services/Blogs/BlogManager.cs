using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;
using Persistence.Repositories.Abstractions;
using System.Linq.Expressions;

namespace Application.Services.Blogs;

public class BlogManager : IBlogService
{
    private readonly IBlogRepository _blogRepository;

    //private readonly AuthorBusinessRules _authorBusinessRules;

    public BlogManager(IBlogRepository authorRepository)
    {
        _blogRepository = authorRepository;
    }

    public BlogManager()
    {
    }

    public async Task<Blog?> GetAsync(
        Expression<Func<Blog, bool>> predicate,
        Func<IQueryable<Blog>, IIncludableQueryable<Blog, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Blog? author = await _blogRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return author;
    }

    public async Task<IPaginate<Blog>?> GetListAsync(
        Expression<Func<Blog, bool>>? predicate = null,
        Func<IQueryable<Blog>, IOrderedQueryable<Blog>>? orderBy = null,
        Func<IQueryable<Blog>, IIncludableQueryable<Blog, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Blog> blogList = await _blogRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return blogList;
    }

    public async Task<Blog> AddAsync(Blog blog)
    {
        Blog addedBlog = await _blogRepository.AddAsync(blog);

        return addedBlog;
    }

    public async Task<Blog> UpdateAsync(Blog blog)
    {
        Blog updatedBlog = await _blogRepository.UpdateAsync(blog);

        return updatedBlog;
    }

    public async Task<Blog> DeleteAsync(Blog blog, bool permanent = false)
    {
        Blog deletedblog = await _blogRepository.DeleteAsync(blog);

        return deletedblog;
    }
}
