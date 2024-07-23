using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Authors.Queries.GetList;

public class GetListByAuthorIdBlogInListItemDto : IDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }

}