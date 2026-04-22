using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace SportsLeague.DataAccess.Repositories
{
    public class TournamentSponsorRepository : GenericRepository<TournamentSponsor>, ITournamentSponsorRepository
    {
        private readonly LeagueDbContext _context;

        public TournamentSponsorRepository(LeagueDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TournamentSponsor>> GetByTournamentIdAsync(int tournamentId)
        {
            return await _context.Set<TournamentSponsor>().Include(x => x.Tournament)
                                                          .Include(x => x.Sponsor)
                                                          .Where(x => x.TournamentId == tournamentId)
                                                          .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int tournamentId, int sponsorId)
        {
            return await _context.Set<TournamentSponsor>().AnyAsync(x => x.TournamentId == tournamentId && x.SponsorId == sponsorId);
        }

        public async Task<IEnumerable<Tournament>> GetOnlyTournamentsIdAsync(int tournamentId)
        {
            var tournament = await _context.Set<TournamentSponsor>().Include(t => t.Tournament).ToListAsync();
            if (tournament is null)
            {
                throw new ArgumentNullException(nameof(tournament));
            }
            return tournament.Select(t => t.Tournament).ToList();
        }
    }
}
