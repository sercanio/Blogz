using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Posts.Queries.GetList;

public class GetListPostQuery : IRequest<GetListResponse<GetListPostDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListQueryHandler : IRequestHandler<GetListPostQuery, GetListResponse<GetListPostDto>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetListQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListPostDto>> Handle(GetListPostQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Post> posts = await _postRepository.GetListAsync(
                include: p => p.Include(p => p.Blog).ThenInclude(b => b.Author).ThenInclude(a => a.User),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: p => p.OrderByDescending(p => p.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPostDto> response = _mapper.Map<GetListResponse<GetListPostDto>>(posts);
            return response;
        }
    }
}