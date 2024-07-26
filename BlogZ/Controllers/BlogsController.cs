using Application.Features.Authors.Commands.Update;
using Application.Features.Authors.Queries.GetById;
using Application.Features.Authors.Queries.GetByUserId;
using Application.Features.Posts.Queries.GetListByBlogId;
using Application.Services.Blogs;
using Blogz.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using WebAPI.Controllers;

namespace Blogz.Controllers
{
    public class BlogsController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediator _mediator;

        public BlogsController(UserManager<IdentityUser> userManager, IMediator mediator, IBlogService blogService)
        {
            _userManager = userManager;
            _mediator = mediator;
            _blogService = blogService;
        }

        [HttpGet("blogs/{username}")]
        public async Task<IActionResult> Blog(string username, int pageIndex = 0, int pageSize = 6)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            GetByUserIdAuthorQuery authorQuery = new() { UserId = user.Id };
            GetByIdAuthorResponse author = await _mediator.Send(authorQuery);

            GetListByBlogIdPostQuery blogPostsQuery = new()
            {
                BlogId = author.Blog.Id,
                PageRequest = new PageRequest()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize
                }
            };

            GetListResponse<GetListByBlogIdPostDto> blogPosts = await _mediator.Send(blogPostsQuery);

            var viewModel = new BlogViewModel
            {
                Author = author,
                BlogPosts = blogPosts
            };

            return View(viewModel);
        }


        [HttpPost("blogs/{username}/UpdateBiography")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBiography(string username, UpdateAuthorCommand model)
        {
            if (ModelState.IsValid)
            {
                UpdateAuthorCommand command = new()
                {
                    Id = model.Id,
                    Biography = model.Biography
                };

                var response = await _mediator.Send(command);

                if (response != null)
                {
                    return RedirectToAction("Blog", "Blogs", new { username });
                }

                ModelState.AddModelError(string.Empty, "Failed to update biography.");
            }

            return RedirectToAction("Blog", "Blogs", new { username });
        }

    }
}
