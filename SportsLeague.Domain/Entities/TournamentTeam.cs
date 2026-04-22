using System.ComponentModel.DataAnnotations;

namespace SportsLeague.Domain.Entities
{
    public class TournamentTeam : AuditBase
    {
        public required int TournamentId { get; set; }
        public required int TeamId { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Tournament Tournament { get; set; } = null!;
        public Teams Team { get; set; } = null!;

    }
}