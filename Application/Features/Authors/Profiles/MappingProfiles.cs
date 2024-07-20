using Application.Features.Authors.Commands.Create;
using Application.Features.Authors.Queries.GetById;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Authors.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

        CreateMap<CreateAuthorCommand, Author>();
        CreateMap<Author, CreatedAuthorResponse>();

        CreateMap<Author, GetByIdAuthorResponse>();
    }
}