using Application.Features.Posts.Queries.GetListByAuthorId;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using WebAPI.Controllers;

namespace Blogz.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index(int pageIndex = 0, int pageSize = 10)
    {
        var query = new GetListPostQuery { PageRequest = new PageRequest { PageIndex = pageIndex, PageSize = pageSize } };
        var response = await Mediator.Send(query);

        return View(response);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
