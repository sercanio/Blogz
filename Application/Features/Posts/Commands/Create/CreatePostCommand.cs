using Application.Services.Blogs;
using Application.Services.ImageService;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    public IFormFile? CoverImage { get; set; }

    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, CreatedPostResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly IBlogService _blogService;
        private readonly ImageServiceBase _imageService;

        public CreatePostCommandHandler(IMapper mapper, IPostRepository postRepository, IBlogService blogService, ImageServiceBase imageService)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _blogService = blogService;
            _postRepository = postRepository;
            _imageService = imageService;
        }

        public async Task<CreatedPostResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            Post post = _mapper.Map<Post>(request);
            post.Slug = GenerateSlug(request.Title);

            if (request.CoverImage != null)
            {
                string imageUrl = await _imageService.UploadAsync(request.CoverImage);
                post.CoverImageURL = imageUrl;
            }

            Post savedPost = await _postRepository.AddAsync(post);
            CreatedPostResponse response = _mapper.Map<CreatedPostResponse>(savedPost);

            return response;
        }

        private static string GenerateSlug(string title)
        {
            string slug = title.ToLowerInvariant();
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", "-").Trim('-');
            slug = slug.Trim('-');
            return slug;
        }
    }
}
