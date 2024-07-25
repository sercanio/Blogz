using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Posts.Queries.GetListByAuthorId;

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
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPostDto> response = _mapper.Map<GetListResponse<GetListPostDto>>(posts);
            return response;
        }
    }
}