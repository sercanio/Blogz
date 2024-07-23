using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Blogs.Queries.GetList;

public class GetListByAuthorIdBlogQuery : IRequest<GetListResponse<GetListByAuthorIdBlogDto>>
{
    public PageRequest PageRequest { get; set; }

    //public string[] Roles => [Admin, Read];

    public class GetListAuthorQueryHandler : IRequestHandler<GetListByAuthorIdBlogQuery, GetListResponse<GetListByAuthorIdBlogDto>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public GetListAuthorQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByAuthorIdBlogDto>> Handle(GetListByAuthorIdBlogQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Author> authors = await _authorRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByAuthorIdBlogDto> response = _mapper.Map<GetListResponse<GetListByAuthorIdBlogDto>>(authors);
            return response;
        }
    }
}