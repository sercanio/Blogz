using Application.Features.Titles.Queries.GetDynamic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Persistence.Dynamic;

namespace WebUI.Controllers
{
    public class AuthorSearchController : Controller
    {
        private readonly IMediator _mediator;

        public AuthorSearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromBody] DynamicQuery dynamic)
        {
            try
            {
                var query = new GetDynamicAuthorQuery
                {
                    PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 },
                    DynamicQuery = dynamic
                };

                var authors = await _mediator.Send(query);

                return PartialView("_SearchAuthorPartial", authors);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Search action: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
