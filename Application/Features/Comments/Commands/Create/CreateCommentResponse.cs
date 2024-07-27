using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Comments.Commands.Create;

public class CreatedCommentResponse : IResponse
{
    public Guid AuthorId { get; set; }
    public Guid PostId { get; set; }
    public string Content { get; set; }

    public Author Author { get; set; }
    public Post Post { get; set; }
}