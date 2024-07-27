using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Authors.Commands.Update
{
    public class UpdateAuthorCommand : IRequest<UpdatedAuthorResponse>, ILoggableRequest, ITransactionalRequest
    {
        public required Guid Id { get; set; }
        public string? ProfileImageURL { get; set; }
        public string? Biography { get; set; }

        public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, UpdatedAuthorResponse>
        {
            private readonly IMapper _mapper;
            private readonly IAuthorRepository _authorRepository;
            private readonly UserManager<IdentityUser> _userManager;

            public UpdateAuthorCommandHandler(IMapper mapper, IAuthorRepository authorRepository, UserManager<IdentityUser> userManager)
            {
                _mapper = mapper;
                _authorRepository = authorRepository;
                _userManager = userManager;
            }

            public async Task<UpdatedAuthorResponse> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
            {
                Author author = _mapper.Map<Author>(request);

                Author? authorInDb = await _authorRepository.GetAsync(
                    predicate: a => a.Id == request.Id,
                    enableTracking: false,
                    cancellationToken: cancellationToken);

                author.UserId = authorInDb.UserId;

                Author updatedAuthor = await _authorRepository.UpdateAsync(author, cancellationToken);

                UpdatedAuthorResponse response = _mapper.Map<UpdatedAuthorResponse>(updatedAuthor);

                return response;
            }
        }
    }
}
