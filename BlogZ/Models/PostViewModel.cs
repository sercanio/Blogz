using Application.Features.Authors.Queries.GetById;
using Application.Features.Posts.Queries.GetBySlug;
using Domain.Entities;

namespace Blogz.Models;

public class PostViewModel
{
    public GetBySlugPostResponse Post { get; set; }
    public GetByIdAuthorResponse Author { get; set; }
    public BlogViewModel Blog { get; set; }
    public IList<Comment> Comments { get; set; }  // Add this line

}
