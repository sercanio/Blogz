using Application.Features.Authors.Queries.GetByUserId;
using Application.Services.Blogs;
using Blogz.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogz.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService; // Dependency for accessing blog data
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediator _mediator;

        public BlogController(UserManager<IdentityUser> userManager, IMediator mediator, IBlogService blogService)
        {
            _userManager = userManager;
            _mediator = mediator;
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            // Get all blogs (replace with specific logic if needed)
            //var blogs = _blogService.GetAllBlogs();

            return View(); // Pass the list of blogs to the view
        }

        public async Task<IActionResult> Details(string username)
        {
            // Get user profile (replace with specific logic)
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) // Handle non-existent user
            {
                return NotFound();
            }

            GetByUserIdAuthorQuery query = new() { UserId = user.Id };
            var author = await _mediator.Send(query);

            // Get blog posts for the user (replace with specific logic)
            //var blogPosts = _blogService.GetBlogPostsByUserId(user.Id);

            // Combine user profile and blog posts into a view model (optional)
            var viewModel = new UserProfileViewModel
            {
                Author = author,
            };

            return View(viewModel); // Pass the view model to the view
        }


        // You can add additional actions here for functionalities like:
        // - Creating new blog posts
        // - Editing existing blog posts
        // - Deleting blog posts (if applicable)
    }
}
