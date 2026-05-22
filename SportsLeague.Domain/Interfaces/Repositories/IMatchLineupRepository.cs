using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories;

public interface IMatchLineupRepository : IGenericRepository<MatchLineup>
{
    Task<bool> ExistsByMatchAndPlayerAsync(int matchId, int playerId);
    Task<int> CountStartersByMatchAndTeamAsync(int matchId, int teamId);
    Task<IEnumerable<MatchLineup>> GetByMatchWithDetailsAsync(int matchId);
    Task<IEnumerable<MatchLineup>> GetByMatchAndTeamWithDetailsAsync(int matchId, int teamId);
    Task<MatchLineup?> GetByIdWithDetailsAsync(int id);
}