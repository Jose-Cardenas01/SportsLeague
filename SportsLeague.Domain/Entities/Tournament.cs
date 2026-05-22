using SportsLeague.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SportsLeague.Domain.Entities
{
    public class Tournament : AuditBase
    {
        [MaxLength(100)]
        [Required]
        public required string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        [Required]
        public required string Season { get; set; } = string.Empty;
        [MaxLength(100)]
        [Required]
        public required DateTime StartDate { get; set; }
        [MaxLength(100)]
        [Required]
        public required DateTime EndDate { get; set; }
        public TournamentStatus Status { get; set; } = TournamentStatus.Pending;

        // Navigation Properties
        public ICollection<TournamentTeam> TournamentTeams { get; set; } = new List<TournamentTeam>();
        public ICollection<TournamentSponsor> TournamentSponsors { get; set; } = new List<TournamentSponsor>();
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
