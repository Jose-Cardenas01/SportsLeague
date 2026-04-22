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

            // Referee mappings
            CreateMap<RefereeRequestDTO, Referee>();
            CreateMap<Referee, RefereeResponseDTO>();

            // Tournament mappings
            CreateMap<TournamentRequestDTO, Tournament>();
            CreateMap<Tournament, TournamentResponseDTO>().ForMember(dest => dest.TeamsCount, opt =>
            {
                opt.MapFrom(src => src.TournamentTeams != null ? src.TournamentTeams.Count : 0);
            });

            // Sponsor mappings
            CreateMap<SponsorRequestDTO, Sponsors>();
            CreateMap<Sponsors, SponsorResponseDTO>();

            // TournamentSponsor mappings
            CreateMap<TournamentSponsorRequestDTO, TournamentSponsor>();
            CreateMap<TournamentSponsor, TournamentSponsorResponseDTO>();
        }
    }
}



