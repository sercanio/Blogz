using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Authors.Commands.Create;

public class CreateAuthorCommand : IRequest<CreatedAuthorResponse>, ILoggableRequest, ITransactionalRequest
{
    public Guid UserId { get; set; }
    public string? Biography { get; set; } = null;
    public string? ProfilePictureUrl { get; set; } = null;

    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, CreatedAuthorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;
        //private readonly AuthorBusinessRules _authorBusinessRules;

        public CreateAuthorCommandHandler(IMapper mapper, IAuthorRepository authorRepository)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        public async Task<CreatedAuthorResponse> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {


            Author author = _mapper.Map<Author>(request);

            var savedAuthor = await _authorRepository.AddAsync(author);

            CreatedAuthorResponse response = _mapper.Map<CreatedAuthorResponse>(savedAuthor);

            return response;
        }
    }
}