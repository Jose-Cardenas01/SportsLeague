using SportsLeague.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.Domain.Entities
{
    public class Sponsors : AuditBase
    {
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string? Phone { get; set; }
        public string? WebsiteUrl { get; set; }
        public SponsorCategory Category { get; set; }

        //Navigator Properties
        public ICollection<TournamentSponsor> TournamentSponsors { get; set; }
    }
}
