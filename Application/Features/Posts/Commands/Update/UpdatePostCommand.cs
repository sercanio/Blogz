
using Application.Services.ImageService;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using NArchitecture.Core.Application.Pipelines.Transaction;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Entries.Commands.Update;

public class UpdatePostCommand : IRequest<UpdatePostResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required string Content { get; set; }
    public required string Title { get; set; }
    public required bool IsPublic { get; set; }
    public IFormFile? CoverImage { get; set; }

    public class UpdatePostCommandCommandHandler : IRequestHandler<UpdatePostCommand, UpdatePostResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly ImageServiceBase _imageService;

        public UpdatePostCommandCommandHandler(IMapper mapper, IPostRepository postRepository, ImageServiceBase imageService)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _imageService = imageService;
        }

        public async Task<UpdatePostResponse> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            Post? post = await _postRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);

            post = _mapper.Map(request, post!);

            if (!string.IsNullOrEmpty(post.CoverImageURL))
            {
                await _imageService.DeleteAsync(post.CoverImageURL);
            }

            if (request.CoverImage != null)
            {
                string imageUrl = await _imageService.UploadAsync(request.CoverImage);
                post.CoverImageURL = imageUrl;
            }

            await _postRepository.UpdateAsync(post!);

            UpdatePostResponse response = _mapper.Map<UpdatePostResponse>(post);
            return response;
        }
    }
}