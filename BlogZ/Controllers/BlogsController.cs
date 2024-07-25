using Application.Features.Authors.Queries.GetById;
using Application.Features.Authors.Queries.GetByUserId;
using Application.Features.Posts.Queries.GetListByAuthorId;
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
        public async Task<IActionResult> Blog(string username)
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
                    PageIndex = 0,
                    PageSize = 10
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
    }
}
