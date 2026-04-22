using SportsLeague.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface ISponsorService
    {
        Task<IEnumerable<Sponsors>> GetAllAsync();
        Task<Sponsors> GetByIdAsync(int id);
        Task<Sponsors> CreateAsync(Sponsors entity);
        Task UpdateAsync(int id, Sponsors entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<Tournament>> GetTournamentsAsync(int sponsorId);
        Task<TournamentSponsor> LinkAsync(int sponsorId, TournamentSponsor entity);
        Task UnbindingAsync(int sponsorId, int tournamentId);
    }
}
