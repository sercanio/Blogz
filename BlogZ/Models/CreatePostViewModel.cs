using Application.Features.Authors.Queries.GetById;

namespace Blogz.Models
{
    public class CreatePostViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublic { get; set; }
        public GetByIdAuthorResponse Author { get; set; }
        public BlogViewModel Blog { get; set; }
    }
}
