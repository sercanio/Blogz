using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Posts.Commands.Create;

public class CreatedPostResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public Guid BlogId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Slug { get; set; }
    public bool IsPublic { get; set; }
    public string CoverImageURL { get; set; }

    public Blog Blog { get; set; }
    public Author Author { get; set; }
}