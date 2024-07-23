using Blogz.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Security.Extensions;
using System.Diagnostics;

namespace WebAPI.Controllers;

public class BaseController : Controller
{
    protected IMediator Mediator =>
        _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>()
            ?? throw new InvalidOperationException("IMediator cannot be retrieved from request services.");

    private IMediator? _mediator;

    protected string getIpAddress()
    {
        string ipAddress = Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"].ToString()
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()
                ?? throw new InvalidOperationException("IP address cannot be retrieved from request.");
        return ipAddress;
    }

    protected Guid getUserIdFromRequest() //todo authentication behavior?
    {
        var userId = Guid.Parse(HttpContext.User.GetIdClaim()!);
        return userId;
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Route("Home/NotFoundHand")]
    public IActionResult NotFoundHandler(int statusCode)
    {
        string message = statusCode == 404 ? "Page not found." : "An unexpected error occurred.";
        return PageNotFound(message);
    }

    public IActionResult PageNotFound(string message = "Page not found.")
    {
        PageNotFoundViewModel model = new PageNotFoundViewModel()
        {
            Message = message,
        };

        return View("PageNotFound", model);
    }

}
