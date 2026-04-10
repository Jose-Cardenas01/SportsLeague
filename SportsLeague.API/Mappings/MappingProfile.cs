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
            //Team mappings
            CreateMap<TeamRequestDTO, Teams>().ReverseMap();
            CreateMap<Teams, TeamResponseDTO>().ReverseMap();

            // Player mappings
            CreateMap<PlayerRequestDTO, Player>();
            CreateMap<Player, PlayerResponseDTO>().ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.Name));
        }
    }
}
