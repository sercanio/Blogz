using Application.Features.Authors.Queries.GetById;
using Application.Features.Posts.Queries.GetBySlug;

namespace Blogz.Models;

public class PostViewModel
{
    public GetBySlugPostResponse Post { get; set; }
    public GetByIdAuthorResponse Author { get; set; }
}
