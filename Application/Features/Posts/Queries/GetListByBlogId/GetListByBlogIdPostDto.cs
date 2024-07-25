using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Posts.Queries.GetListByAuthorId;

public class GetListByBlogIdPostDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Slug { get; set; }
    public bool IsPublic { get; set; }
    public string CoverImageURL { get; set; }
    public DateTime CreatedDate { get; set; }

    public ICollection<Comment> Comments { get; set; }
    public ICollection<Tag> Tags { get; set; }
}