using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories;

public class MatchLineupRepository : GenericRepository<MatchLineup>, IMatchLineupRepository
{
    public MatchLineupRepository(LeagueDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsByMatchAndPlayerAsync(int matchId, int playerId)
    {
        return await _context.Set<MatchLineup>()
            .AnyAsync(ml => ml.MatchId == matchId && ml.PlayerId == playerId);
    }

    public async Task<int> CountStartersByMatchAndTeamAsync(int matchId, int teamId)
    {
        return await _context.Set<MatchLineup>()
            .Where(ml => ml.MatchId == matchId && ml.IsStarter && ml.Player.TeamId == teamId)
            .CountAsync();
    }

    public async Task<IEnumerable<MatchLineup>> GetByMatchWithDetailsAsync(int matchId)
    {
        return await _context.Set<MatchLineup>()
            .Where(ml => ml.MatchId == matchId)
            .Include(ml => ml.Player)
            .ThenInclude(p => p.Team)
            .OrderByDescending(ml => ml.IsStarter)
            .ThenBy(ml => ml.Player.Number)
            .ToListAsync();
    }

    public async Task<IEnumerable<MatchLineup>> GetByMatchAndTeamWithDetailsAsync(int matchId, int teamId)
    {
        return await _context.Set<MatchLineup>()
            .Where(ml => ml.MatchId == matchId && ml.Player.TeamId == teamId)
            .Include(ml => ml.Player)
            .ThenInclude(p => p.Team)
            .OrderByDescending(ml => ml.IsStarter)
            .ThenBy(ml => ml.Player.Number)
            .ToListAsync();
    }

    public async Task<MatchLineup?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Set<MatchLineup>()
            .Include(ml => ml.Player)
            .ThenInclude(p => p.Team)
            .FirstOrDefaultAsync(ml => ml.Id == id);
    }
}