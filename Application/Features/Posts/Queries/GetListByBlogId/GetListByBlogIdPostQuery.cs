using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Posts.Queries.GetListByBlogId;

public class GetListByBlogIdPostQuery : IRequest<GetListResponse<GetListByBlogIdPostDto>>
{
    public PageRequest PageRequest { get; set; }
    public Guid BlogId { get; set; }

    public class GetListAuthorQueryHandler : IRequestHandler<GetListByBlogIdPostQuery, GetListResponse<GetListByBlogIdPostDto>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetListAuthorQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByBlogIdPostDto>> Handle(GetListByBlogIdPostQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Post> posts = await _postRepository.GetListAsync(
                predicate: p => p.BlogId == request.BlogId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: p => p.OrderByDescending(p => p.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByBlogIdPostDto> response = _mapper.Map<GetListResponse<GetListByBlogIdPostDto>>(posts);
            return response;
        }
    }
}