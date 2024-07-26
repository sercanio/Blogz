using Application.Features.Blogs.Queries.GetById;
using Application.Features.Blogs.Queries.GetList;
using Application.Features.Posts.Queries.GetListByBlogId;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Blogs.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

        CreateMap<Blog, GetByIdBlogResponse>();

        CreateMap<Blog, GetListByBlogIdPostDto>();
        CreateMap<IPaginate<Blog>, GetListResponse<GetListByAuthorIdBlogDto>>();
    }
}