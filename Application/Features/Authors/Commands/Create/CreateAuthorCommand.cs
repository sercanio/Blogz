using Application.Features.Authors.Rules;
using Application.Services.Blogs;
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
        private readonly AuthorBusinessRules _authorBusinessRules;
        private readonly IBlogService _blogService;

        public CreateAuthorCommandHandler(IMapper mapper, IAuthorRepository authorRepository, AuthorBusinessRules authorBusinessRules, IBlogService blogService)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
            _authorBusinessRules = authorBusinessRules;
            _blogService = blogService;
        }

        public async Task<CreatedAuthorResponse> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            // Create Author
            Author author = _mapper.Map<Author>(request);
            var savedAuthor = await _authorRepository.AddAsync(author);

            if (savedAuthor == null)
            {
                throw new Exception("Author creation failed");
            }

            // Create Blog
            Blog blog = new()
            {
                AuthorId = savedAuthor.Id,
            };

            var addedBlog = await _blogService.AddAsync(blog);

            if (addedBlog == null)
            {
                throw new Exception("Blog creation failed");
            }

            CreatedAuthorResponse response = _mapper.Map<CreatedAuthorResponse>(savedAuthor);
            return response;
        }
    }

}