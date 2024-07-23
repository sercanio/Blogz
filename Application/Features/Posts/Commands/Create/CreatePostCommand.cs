using Application.Services.Blogs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using Persistence.Repositories.Abstractions;
using System.Text.RegularExpressions;

namespace Application.Features.Posts.Commands.Create;

public class CreatePostCommand : IRequest<CreatedPostResponse>, ILoggableRequest, ITransactionalRequest
{
    public Guid BlogId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string? Slug { get; set; }
    public bool IsPublic { get; set; } = true;
    public string CoverImageURL { get; set; } = string.Empty;


    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, CreatedPostResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly IBlogService _blogService;

        public CreatePostCommandHandler(IMapper mapper, IPostRepository postRepository, IBlogService blogService)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _blogService = blogService;
            _postRepository = postRepository;
        }

        public async Task<CreatedPostResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            Post post = _mapper.Map<Post>(request);
            post.Slug = GenerateSlug(request.Title);

            Post savedPost = await _postRepository.AddAsync(post);

            CreatedPostResponse response = _mapper.Map<CreatedPostResponse>(savedPost);

            return response;
        }
    }

    private static string GenerateSlug(string title)
    {
        // Convert to lowercase
        string slug = title.ToLowerInvariant();

        // Remove invalid characters
        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

        // Replace spaces with hyphens
        slug = Regex.Replace(slug, @"\s+", "-").Trim('-');

        // Trim hyphens from start and end
        slug = slug.Trim('-');

        return slug;
    }

}