using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Authors.Queries.GetList;

public class GetListAuthorQuery : IRequest<GetListResponse<GetListAuthorListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    //public string[] Roles => [Admin, Read];

    public class GetListAuthorQueryHandler : IRequestHandler<GetListAuthorQuery, GetListResponse<GetListAuthorListItemDto>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public GetListAuthorQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAuthorListItemDto>> Handle(GetListAuthorQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Author> authors = await _authorRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAuthorListItemDto> response = _mapper.Map<GetListResponse<GetListAuthorListItemDto>>(authors);
            return response;
        }
    }
}