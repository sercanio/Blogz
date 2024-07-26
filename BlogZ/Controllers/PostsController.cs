using Application.Features.Authors.Queries.GetById;
using Application.Features.Authors.Queries.GetByUserId;
using Application.Features.Entries.Commands.Update;
using Application.Features.Posts.Commands.Create;
using Application.Features.Posts.Commands.Delete;
using Application.Features.Posts.Queries.GetBySlug;
using Blogz.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace Blogz.Controllers
{
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
            if (string.IsNullOrEmpty(username)) return BadRequest("Username cannot be null or empty");

            IdentityUser? user = await _userManager.FindByNameAsync(username);
            if (user == null) return NotFound();

            var authorQuery = new GetByUserIdAuthorQuery { UserId = user.Id };
            var author = await _mediator.Send(authorQuery);
            if (author == null) return NotFound("Author not found");

            if (User.Identity.Name != username) return Forbid();

            var viewModel = new CreatePostViewModel
            {
                Title = "",
                Content = "",
                IsPublic = false,
                Blog = new BlogViewModel
                {
                    Author = author
                }
            };
            return View(viewModel);
        }

        [HttpPost("posts/{username}/create")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string username, CreatePostCommand model, IFormFile coverImage)
        {
            if (string.IsNullOrEmpty(username)) return BadRequest("Username cannot be null or empty");
            if (User.Identity.Name != username) return Forbid();

            if (!ModelState.IsValid) return View(model); // Ensure model is correctly passed to view

            IdentityUser? user = await _userManager.FindByNameAsync(username);
            if (user == null) return NotFound();

            var authorQuery = new GetByUserIdAuthorQuery { UserId = user.Id };
            var author = await _mediator.Send(authorQuery);
            if (author == null) return NotFound("Author not found");

            var command = new CreatePostCommand
            {
                Title = model.Title,
                Content = model.Content,
                BlogId = author.Blog.Id,
                IsPublic = model.IsPublic,
                CoverImage = coverImage
            };

            var result = await _mediator.Send(command);
            return RedirectToAction("Post", new { username, slug = result.Slug });
        }

        [HttpGet("posts/{username}/edit/{slug}")]
        public async Task<IActionResult> Edit(string username, string slug)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(slug))
            {
                return BadRequest("Username or slug cannot be null or empty");
            }

            // Ensure the authenticated user is the author or in the correct role
            var currentUserName = User.Identity.Name;
            if (currentUserName != username && !User.IsInRole("Moderator") && !User.IsInRole("Admin"))
            {
                return Forbid();
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
            EditPostViewModel viewModel = new()
            {
                Title = post.Title,
                Content = post.Content,
                Slug = post.Slug,
                IsPublic = post.IsPublic,
                Author = author,
                Blog = new BlogViewModel()
                {
                    Author = author
                }
            };

            return View(viewModel);
        }


        [HttpPost("posts/{username}/edit/{slug}")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string username, string slug, UpdatePostCommand model)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(slug))
            {
                return BadRequest("Username or slug cannot be null or empty");
            }

            if (User.Identity.Name != username)
            {
                return Forbid();
            }

            GetBySlugPostQuery postQuery = new() { Slug = slug };
            GetBySlugPostResponse? post = await _mediator.Send(postQuery);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (post == null)
            {
                return NotFound();
            }

            UpdatePostCommand command = new()
            {
                Id = post.Id,
                Title = model.Title,
                Content = model.Content,
                IsPublic = model.IsPublic,
            };

            await _mediator.Send(command);

            return RedirectToAction("Post", new { username, slug });
        }

        [HttpDelete("posts/delete/{slug}")]
        [Authorize]
        public async Task<IActionResult> Delete(string slug)
        {
            // Ensure the user is authenticated
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Forbid();
            }

            // Get the post by slug to find its ID and author
            GetBySlugPostQuery postQuery = new() { Slug = slug };
            GetBySlugPostResponse? post = await _mediator.Send(postQuery);

            if (post == null)
            {
                return NotFound();
            }

            // Check if the user is authorized to delete the post
            if (User.Identity.Name != post.Blog?.Author?.User?.UserName &&
                !User.IsInRole("Moderator") &&
                !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            // Create delete command
            var deleteCommand = new DeletePostCommand { Id = post.Id };
            var result = await _mediator.Send(deleteCommand);

            if (result.Success)
            {
                // Redirect to the user's blog page after successful deletion
                return RedirectToAction("Blog", "Blogs", new { username = post.Blog?.Author?.User?.UserName });
            }

            // If deletion fails, redirect to an error page or the post's details page
            return RedirectToAction("Error", "Home", new { message = result.Message });
        }


        [HttpGet("blogs/{username}/post/{slug}")]
        public async Task<IActionResult> Post(string username, string slug)
        {
            if (string.IsNullOrEmpty(username)) return BadRequest("Username cannot be null or empty");
            if (string.IsNullOrEmpty(slug)) return BadRequest("Slug cannot be null or empty");

            var postQuery = new GetBySlugPostQuery { Slug = slug };
            var post = await _mediator.Send(postQuery);
            if (post == null) return NotFound();

            var author = post.Blog?.Author;
            if (author == null) return NotFound();

            var viewModel = new PostViewModel
            {
                Post = post,
                Author = author,
                Blog = new BlogViewModel
                {
                    Author = author
                }
            };
            return View(viewModel);
        }
    }
}
