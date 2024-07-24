using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Authors.Queries.GetById;

public class GetByIdAuthorQuery : IRequest<GetByIdAuthorResponse>
{
    public Guid Id { get; set; }

    public class GetByIdAuthorQueryHandler : IRequestHandler<GetByIdAuthorQuery, GetByIdAuthorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;

        public GetByIdAuthorQueryHandler(IMapper mapper, IAuthorRepository authorRepository)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        public async Task<GetByIdAuthorResponse> Handle(GetByIdAuthorQuery request, CancellationToken cancellationToken)
        {
            Author? author = await _authorRepository.GetAsync(
                 include: a => a.Include(a => a.Blog).ThenInclude(b => b.Posts)
                                .Include(a => a.User),
                predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);

            GetByIdAuthorResponse response = _mapper.Map<GetByIdAuthorResponse>(author);

            return response;
        }
    }
}