using Application.Features.Authors.Queries.GetById;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Blogs.Queries.GetById;

public class GetByIdBlogResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }

    public DateTime CreatedDate { get; set; }
    public GetByIdAuthorResponse Author { get; set; }
}