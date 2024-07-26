using Application.Features.Authors.Queries.GetById;
using Blogz.Models;

public class EditPostViewModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Slug { get; set; }
    public bool IsPublic { get; set; }
    public GetByIdAuthorResponse Author { get; set; }
    public BlogViewModel Blog { get; set; }
    public string CoverImageUrl { get; set; }
    public IFormFile? CoverImage { get; set; }
}
