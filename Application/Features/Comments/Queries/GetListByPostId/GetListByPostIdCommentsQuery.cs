using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Comments.Queries.GetListByPostId;

public class GetListByPostIdCommentsQuery : IRequest<GetListResponse<GetListByPostIdCommentsDto>>
{
    public PageRequest PageRequest { get; set; }
    public Guid PostId { get; set; }

    public class GetListByPostIdCommentsQueryHandler : IRequestHandler<GetListByPostIdCommentsQuery, GetListResponse<GetListByPostIdCommentsDto>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetListByPostIdCommentsQueryHandler(IMapper mapper, ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByPostIdCommentsDto>> Handle(GetListByPostIdCommentsQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Comment> posts = await _commentRepository.GetListAsync(
                predicate: p => p.PostId == request.PostId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: p => p.OrderByDescending(p => p.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByPostIdCommentsDto> response = _mapper.Map<GetListResponse<GetListByPostIdCommentsDto>>(posts);
            return response;
        }
    }
}