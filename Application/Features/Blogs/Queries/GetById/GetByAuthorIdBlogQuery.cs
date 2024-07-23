using Application.Features.Blogs.Queries.GetById;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Blogs.Queries.GetByUserId;

public class GetByAuthorIdBlogQuery : IRequest<GetByIdBlogResponse>
{
    public required string UserId { get; set; }

    public class GetByUserIdAuthorQueryHandler : IRequestHandler<GetByAuthorIdBlogQuery, GetByIdBlogResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;

        public GetByUserIdAuthorQueryHandler(IMapper mapper, IAuthorRepository authorRepository)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        public async Task<GetByIdBlogResponse> Handle(GetByAuthorIdBlogQuery request, CancellationToken cancellationToken)
        {
            Author? author = await _authorRepository.GetAsync(
                predicate: a => a.UserId == request.UserId,
                include: a => a.Include(a => a.Blog).ThenInclude(b => b.Posts),
                cancellationToken: cancellationToken);

            GetByIdBlogResponse response = _mapper.Map<GetByIdBlogResponse>(author);

            return response;
        }
    }
}
