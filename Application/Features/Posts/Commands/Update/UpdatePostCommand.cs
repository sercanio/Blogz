
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Transaction;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Entries.Commands.Update;

public class UpdatePostCommand : IRequest<UpdatePostResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required string Content { get; set; }
    public required string Title { get; set; }
    public required bool IsPublic { get; set; }

    public class UpdateEntryCommandHandler : IRequestHandler<UpdatePostCommand, UpdatePostResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public UpdateEntryCommandHandler(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<UpdatePostResponse> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            Post? post = await _postRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);

            post = _mapper.Map(request, post!);

            await _postRepository.UpdateAsync(post!);

            UpdatePostResponse response = _mapper.Map<UpdatePostResponse>(post);
            return response;
        }
    }
}