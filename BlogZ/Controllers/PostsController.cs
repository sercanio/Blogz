using Application.Features.Authors.Queries.GetById;
using Application.Features.Authors.Queries.GetByUserId;
using Application.Features.Posts.Commands.Create;
using Application.Features.Posts.Queries.GetBySlug;
using Blogz.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace Blogz.Controllers;

public class PostsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly UserManager<IdentityUser> _userManager;

    public PostsController(IMediator mediator, UserManager<IdentityUser> userManager)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _userManager = userManager;
    }

    [HttpGet("posts/{username}/create")]
    [Authorize]
    public async Task<IActionResult> Create(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return BadRequest("Username cannot be null or empty");
        }

        IdentityUser? user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return NotFound();
        }

        GetByUserIdAuthorQuery authorQuery = new() { UserId = user.Id };
        GetByIdAuthorResponse? author = await _mediator.Send(authorQuery);

        if (author == null)
        {
            return NotFound("Author not found");
        }

        // Ensure the authenticated user is the author
        if (User.Identity.Name != username)
        {
            return Forbid();
        }

        CreatePostViewModel viewModel = new()
        {
            Author = author
        };


        return View(viewModel);
    }

    [HttpPost("posts/{username}/create")]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string username, CreatePostCommand model)
    {
        if (string.IsNullOrEmpty(username))
        {
            return BadRequest("Username cannot be null or empty");
        }

        // Ensure the authenticated user is the author
        if (User.Identity.Name != username)
        {
            return Forbid();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        IdentityUser? user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return NotFound();
        }

        GetByUserIdAuthorQuery authorQuery = new() { UserId = user.Id };
        GetByIdAuthorResponse? author = await _mediator.Send(authorQuery);

        if (author == null)
        {
            return NotFound("Author not found");
        }

        // Create a new post command
        CreatePostCommand command = new()
        {
            Title = model.Title,
            Content = model.Content,
            BlogId = author.Blog.Id,
            IsPublic = model.IsPublic
        };

        CreatedPostResponse? result = await _mediator.Send(command);

        return RedirectToAction("Post", new { username = username, slug = result.Slug });
    }


    [HttpGet("blogs/{username}/post/{slug}")]
    public async Task<IActionResult> Post(string username, string slug)
    {

        if (string.IsNullOrEmpty(username))
        {
            return BadRequest("Username cannot be null or empty");
        }

        if (string.IsNullOrEmpty(slug))
        {
            return BadRequest("Slug cannot be null or empty");
        }

        // Get the post by slug
        GetBySlugPostQuery postQuery = new() { Slug = slug };
        GetBySlugPostResponse? post = await _mediator.Send(postQuery);

        if (post == null)
        {
            return NotFound();
        }

        // Assuming the post object has an Author property
        GetByIdAuthorResponse? author = post.Blog?.Author;

        if (author == null)
        {
            return NotFound();
        }

        // Create a view model and pass it to the view
        PostViewModel viewModel = new()
        {
            Post = post,
            Author = author
        };

        return View(viewModel);
    }
}
