using Application.Features.Authors.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Blogs.Queries.GetById;

public class GetByIdBlogQuery : IRequest<GetByIdBlogResponse>
{
    public Guid Id { get; set; }

    public class GetByIdBlogQueryHandler : IRequestHandler<GetByIdBlogQuery, GetByIdBlogResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;
        private readonly AuthorBusinessRules _authorBusinessRules;

        public GetByIdBlogQueryHandler(IMapper mapper, AuthorBusinessRules authorBusinessRules, IBlogRepository blogRepository)
        {
            _mapper = mapper;
            _blogRepository = blogRepository;
            _authorBusinessRules = authorBusinessRules;
        }

        public async Task<GetByIdBlogResponse> Handle(GetByIdBlogQuery request, CancellationToken cancellationToken)
        {
            Blog? blog = await _blogRepository.GetAsync(
                 include: a => a.Include(b => b.Posts).ThenInclude(p => p.Comments),
                predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);

            GetByIdBlogResponse response = _mapper.Map<GetByIdBlogResponse>(blog);

            return response;
        }
    }
}