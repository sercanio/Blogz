using Application.Features.Auth.Commands.Register;
using AutoMapper;

namespace Application.Features.Auth.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RegisterCommand, RegisteredResponse>().ReverseMap();
    }
}