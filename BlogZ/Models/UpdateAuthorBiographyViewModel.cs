using Application.Features.Authors.Queries.GetById;

namespace Blogz.Models;

public class UpdateAuthorBiographyViewModel
{
    public GetByIdAuthorResponse Author { get; set; }
    public string Biography { get; set; }
}