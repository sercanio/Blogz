using Application.Features.Authors.Queries.GetById;
using Domain.Entities;

namespace Blogz.Models;

public class UserProfileViewModel
{
    public GetByIdAuthorResponse Author { get; set; }
    public IEnumerable<Post> BlogPosts { get; set; }
}