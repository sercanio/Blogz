using Application.Features.Authors.Queries.GetById;
using Blogz.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents;

public class UpdateAuthorBiography : ViewComponent
{
    public IViewComponentResult Invoke(GetByIdAuthorResponse author)
    {
        UpdateAuthorBiographyViewModel viewModel = new()
        {
            Author = author,
        };

        return View(viewModel);
    }
}
