using Application.Features.Authors.Commands.Create;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Authors.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

        CreateMap<CreateAuthorCommand, Author>();
        CreateMap<Author, CreatedAuthorResponse>();
    }
}