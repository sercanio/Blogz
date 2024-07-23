using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Authors.Queries.GetList;

public class GetListByAuthorIdBlogDto : IDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string? Biography { get; set; }
    public string? ProfilePictureUrl { get; set; }

}