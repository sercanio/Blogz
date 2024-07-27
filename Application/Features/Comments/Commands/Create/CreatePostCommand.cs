using Application.Features.Comments.Commands.Create;
using Application.Services.Authors;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Posts.Commands.Create;

public class CreateCommentCommand : IRequest<CreatedCommentResponse>, ILoggableRequest, ITransactionalRequest
{
    public required Guid AuthorId { get; set; }
    public required Guid PostId { get; set; }
    public required string Content { get; set; }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CreatedCommentResponse>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public CreateCommentCommandHandler(IMapper mapper, IAuthorService authorService, ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            _authorService = authorService;
            _mapper = mapper;
        }

        public async Task<CreatedCommentResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            Comment comment = _mapper.Map<Comment>(request);

            Comment savedComment = await _commentRepository.AddAsync(comment, cancellationToken: cancellationToken);

            var author = await _authorService.GetAsync(
                        predicate: a => a.Id == savedComment.AuthorId,
                        include: a => a.Include(a => a.User));

            savedComment.Author = author!;

            CreatedCommentResponse response = _mapper.Map<CreatedCommentResponse>(savedComment);

            return response;
        }
    }
}
