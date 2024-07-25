using Application.Features.Entries.Commands.Update;
using Application.Features.Posts.Commands.Create;
using Application.Features.Posts.Queries.GetById;
using Application.Features.Posts.Queries.GetBySlug;
using Application.Features.Posts.Queries.GetListByAuthorId;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Posts.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

        CreateMap<CreatePostCommand, Post>();
        CreateMap<Post, CreatedPostResponse>();

        CreateMap<UpdatePostCommand, Post>();
        CreateMap<Post, UpdatePostResponse>();

        CreateMap<Post, GetByIdPostResponse>();
        CreateMap<Post, GetBySlugPostResponse>();

        CreateMap<Post, GetListByBlogIdPostDto>();
        CreateMap<IPaginate<Post>, GetListResponse<GetListByBlogIdPostDto>>();
    }
}