using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Persistence.Dynamic;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Titles.Queries.GetDynamic;
public class GetDynamicAuthorQuery : IRequest<List<Author>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }

    public class GetDynamicAuthorQueryHandler : IRequestHandler<GetDynamicAuthorQuery, List<Author>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public GetDynamicAuthorQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<List<Author>> Handle(GetDynamicAuthorQuery request, CancellationToken cancellationToken)
        {
            var authors = await _authorRepository.GetListByDynamicAsync(request.DynamicQuery, index: request.PageRequest.PageIndex, size: request.PageRequest.PageSize);

            var response = _mapper.Map<List<Author>>(authors);

            return response;
        }
    }
}
