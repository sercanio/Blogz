using Application.Features.Posts.Queries.GetList;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
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
        GetListPostQuery query = new GetListPostQuery { PageRequest = new PageRequest { PageIndex = pageIndex, PageSize = pageSize } };
        GetListResponse<GetListPostDto> response = await Mediator.Send(query);

        return View(response);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
