using Application.Features.Authors.Queries.GetById;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Authors.Queries.GetByUserId;

public class GetByUserIdAuthorQuery : IRequest<GetByIdAuthorResponse>
{
    public required string UserId { get; set; }

    public class GetByUserIdAuthorQueryHandler : IRequestHandler<GetByUserIdAuthorQuery, GetByIdAuthorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;

        public GetByUserIdAuthorQueryHandler(IMapper mapper, IAuthorRepository authorRepository)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        public async Task<GetByIdAuthorResponse> Handle(GetByUserIdAuthorQuery request, CancellationToken cancellationToken)
        {
            Author? author = await _authorRepository.GetAsync(
                predicate: a => a.UserId == request.UserId,
                include: a => a.Include(a => a.Blog).ThenInclude(b => b.Posts),
            cancellationToken: cancellationToken);

            GetByIdAuthorResponse response = _mapper.Map<GetByIdAuthorResponse>(author);

            return response;
        }
    }
}
