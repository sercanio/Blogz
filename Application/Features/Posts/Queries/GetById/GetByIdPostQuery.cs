using Application.Features.Authors.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Posts.Queries.GetById;

public class GetByIdPostQuery : IRequest<GetByIdPostResponse>
{
    public Guid Id { get; set; }

    public class GetByIdPostQueryHandler : IRequestHandler<GetByIdPostQuery, GetByIdPostResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly AuthorBusinessRules _authorBusinessRules;

        public GetByIdPostQueryHandler(IMapper mapper, AuthorBusinessRules authorBusinessRules, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _authorBusinessRules = authorBusinessRules;
        }

        public async Task<GetByIdPostResponse> Handle(GetByIdPostQuery request, CancellationToken cancellationToken)
        {
            Post? post = await _postRepository.GetAsync(
                 include: p => p.Include(p => p.Comments)
                 .Include(p => p.Blog).ThenInclude(b => b.Author),
                predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);

            GetByIdPostResponse response = _mapper.Map<GetByIdPostResponse>(post);

            return response;
        }
    }
}