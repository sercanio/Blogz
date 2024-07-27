using Application.Features.Authors.Queries.GetById;
using Application.Features.Authors.Queries.GetByUserId;
using Application.Features.Posts.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Controllers;

namespace WebUI.Controllers
{
    public class CommentsController : BaseController
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("posts/comment")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostComment(CreateCommentCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid comment data.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            GetByUserIdAuthorQuery autorQueryByUserId = new() { UserId = userId };
            GetByIdAuthorResponse author = await _mediator.Send(autorQueryByUserId);

            command.AuthorId = author.Id;

            var result = await _mediator.Send(command);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating comment.");
            }

            return Json(new
            {
                success = true,
                message = "Comment added successfully!",
                comment = new
                {
                    result.AuthorId,
                    result.PostId,
                    result.Content,
                    result.Author,
                    result.Post
                }
            });
        }
    }
}
