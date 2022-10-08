using AutoMapper;
using WAPP.API.Dtos;
using WAPP.API.Models;

namespace WAPP.API.Profiles;

public class CommandProfile : Profile
{
    public CommandProfile()
    {
        // Source -> Target
        CreateMap<Command, CommandReadDto>();
        CreateMap<CommandCreateDto, Command>();
        CreateMap<CommandUpdateDto, Command>();
    }
}