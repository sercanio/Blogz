using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents;

public class SearchAuthor : ViewComponent
{
    public IViewComponentResult Invoke(List<Author> author)
    {
        SearchAuthorViewModel viewModel = new()
        {
            Authors = author,
        };

        return View(viewModel);
    }
}
