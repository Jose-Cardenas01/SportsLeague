using SportsLeague.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ITournamentSponsorRepository : IGenericRepository<TournamentSponsor>
    {
        Task<IEnumerable<TournamentSponsor>> GetByTournamentIdAsync(int tournamentId);
        Task<IEnumerable<Tournament>> GetOnlyTournamentsIdAsync(int tournamentId);
        Task<bool> ExistsAsync(int tournamentId, int sponsorId);
    }
}
