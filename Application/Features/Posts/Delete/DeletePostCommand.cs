using Application.Features.Entries.Commands.Update;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Posts.Commands.Delete;

public class DeletePostCommand : IRequest<DeletePostResponse>, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, DeletePostResponse>
    {
        private readonly IPostRepository _postRepository;

        public DeletePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<DeletePostResponse> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {

            Post? post = await _postRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);

            if (post == null)
            {
                return new DeletePostResponse
                {
                    Success = false,
                    Message = "Post not found"
                };
            }

            await _postRepository.DeleteAsync(post, cancellationToken: cancellationToken);

            return new DeletePostResponse
            {
                Success = true,
                Message = "Post deleted successfully"
            };
        }
    }
}