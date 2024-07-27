using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Comments.Queries.GetListByPostId;

public class GetListByPostIdCommentsDto : IDto
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string Content { get; set; }

    public Author Author { get; set; }
}