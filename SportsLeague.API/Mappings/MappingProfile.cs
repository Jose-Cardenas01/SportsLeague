using AutoMapper;
using Sportsleague.API.DTOs.Request;
using Sportsleague.API.DTOs.Response;
using SportsLeague.Domain.Entities;

namespace Sportsleague.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TeamRequestDTO, Teams>().ReverseMap();
            CreateMap<Teams, TeamResponseDTO>().ReverseMap();
        }
    }
}
