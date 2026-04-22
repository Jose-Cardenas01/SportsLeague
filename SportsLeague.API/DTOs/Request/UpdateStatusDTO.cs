using SportsLeague.Domain.Enum;

namespace Sportsleague.API.DTOs.Request
{
    public class UpdateStatusDTO
    {
        public TournamentStatus Status { get; set; }
    }
}
