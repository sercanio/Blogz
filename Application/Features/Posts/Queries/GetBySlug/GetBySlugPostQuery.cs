using Application.Features.Authors.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Posts.Queries.GetBySlug;

public class GetBySlugPostQuery : IRequest<GetBySlugPostResponse>
{
    public string Slug { get; set; }

    public class GetBySlugPostQueryHandler : IRequestHandler<GetBySlugPostQuery, GetBySlugPostResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly AuthorBusinessRules _authorBusinessRules;

        public GetBySlugPostQueryHandler(IMapper mapper, AuthorBusinessRules authorBusinessRules, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _authorBusinessRules = authorBusinessRules;
        }

        public async Task<GetBySlugPostResponse> Handle(GetBySlugPostQuery request, CancellationToken cancellationToken)
        {
            Post? post = await _postRepository.GetAsync(
                include: p => p.Include(p => p.Comments)
                               .Include(p => p.Blog).ThenInclude(b => b.Author).ThenInclude(a => a.User),
                predicate: a => a.Slug == request.Slug, cancellationToken: cancellationToken);

            GetBySlugPostResponse response = _mapper.Map<GetBySlugPostResponse>(post);

            return response;
        }
    }
}
