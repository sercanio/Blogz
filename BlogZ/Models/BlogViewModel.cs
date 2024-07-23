using Application.Features.Authors.Queries.GetById;
using Application.Features.Posts.Queries.GetListByAuthorId;
using NArchitecture.Core.Application.Responses;

namespace Blogz.Models;

public class BlogViewModel
{
    public GetByIdAuthorResponse Author { get; set; }
    public GetListResponse<GetListByBlogIdPostDto> BlogPosts { get; set; }
}