using Application.Features.Comments.Commands.Create;
using Application.Features.Comments.Queries.GetListByPostId;
using Application.Features.Posts.Commands.Create;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Comments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateCommentCommand, Comment>();
        CreateMap<Comment, CreatedCommentResponse>();

        CreateMap<Comment, GetListByPostIdCommentsDto>();
        CreateMap<IPaginate<Comment>, GetListResponse<GetListByPostIdCommentsDto>>();
    }
}