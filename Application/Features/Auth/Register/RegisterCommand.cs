using Application.Services.Authors;
using Application.Services.Blogs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NArchitecture.Core.Application.Pipelines.Transaction;
using NArchitecture.Core.Security.Hashing;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisteredResponse>, ITransactionalRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string UserName { get; set; }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredResponse>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthorService _authorService;
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IAuthorService authorService, IMapper mapper, IBlogService blogService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authorService = authorService;
            _blogService = blogService;
            _mapper = mapper;
        }

        public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            HashingHelper.CreatePasswordHash(
                request.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            var identityUser = new IdentityUser
            {
                Email = request.Email,
                PasswordHash = request.Password,
                UserName = request.UserName
            };
            IdentityUser identityUserToSave = _mapper.Map<IdentityUser>(identityUser);
            var identityResult = await _userManager.CreateAsync(identityUserToSave, request.Password);

            RegisteredResponse registeredResponse = new() { IdentityResult = identityResult };

            if (!identityResult.Succeeded)
            {
                return registeredResponse;
            }

            var createdUser = await _userManager.FindByEmailAsync(request.Email);

            var author = new Author()
            {
                Biography = $"This is {request.UserName}'s biography.",
                CreatedDate = DateTime.UtcNow,
                ProfileImageURL = string.Empty,
                UserId = createdUser.Id,
            };
            Author authorToSave = _mapper.Map<Author>(author);
            await _authorService.AddAsync(authorToSave);

            var blog = new Blog()
            {
                AuthorId = author.Id,
                CreatedDate = DateTime.Now,
            };
            Blog blogToSave = _mapper.Map<Blog>(blog);
            await _blogService.AddAsync(blogToSave);

            await _userManager.AddToRoleAsync(identityUser, "Author");

            return registeredResponse;
        }
    }
}
