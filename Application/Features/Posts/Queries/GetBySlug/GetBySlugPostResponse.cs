using Application.Features.Authors.Queries.GetById;
using Application.Features.Blogs.Queries.GetById;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Posts.Queries.GetBySlug;

public class GetBySlugPostResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BlogId { get; set; }
    public Guid AuthorId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Slug { get; set; }
    public bool IsPublic { get; set; }
    public string CoverImageURL { get; set; }
    public DateTime CreatedDate { get; set; }

    public GetByIdBlogResponse Blog { get; set; }
    public GetByIdAuthorResponse Author { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Tag> Tags { get; set; }
}