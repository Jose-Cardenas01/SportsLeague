using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services;

public class MatchLineupService : IMatchLineupService
{
    private readonly IMatchRepository _matchRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IMatchLineupRepository _matchLineupRepository;
    private readonly ILogger<MatchLineupService> _logger;

    public MatchLineupService(
        IMatchRepository matchRepository,
        IPlayerRepository playerRepository,
        IMatchLineupRepository matchLineupRepository,
        ILogger<MatchLineupService> logger)
    {
        _matchRepository = matchRepository;
        _playerRepository = playerRepository;
        _matchLineupRepository = matchLineupRepository;
        _logger = logger;
    }

    public async Task<MatchLineup> RegisterLineupPlayerAsync(int matchId, MatchLineup lineup)
    {
        var match = await _matchRepository.GetByIdasync(matchId)
            ?? throw new KeyNotFoundException($"No se encontró el partido con ID {matchId}");

        if (match.Status != MatchStatus.Scheduled)
            throw new InvalidOperationException("Solo se pueden registrar alineaciones en partidos Scheduled");

        var player = await _playerRepository.GetByIdasync(lineup.PlayerId)
            ?? throw new KeyNotFoundException($"No se encontró el jugador con ID {lineup.PlayerId}");

        if (player.TeamId != match.HomeTeamId && player.TeamId != match.AwayTeamId)
            throw new InvalidOperationException("El jugador no pertenece a ninguno de los equipos del partido");

        if (await _matchLineupRepository.ExistsByMatchAndPlayerAsync(matchId, lineup.PlayerId))
            throw new InvalidOperationException("El jugador ya está registrado en la alineación de este partido");

        if (lineup.IsStarter)
        {
            var starters = await _matchLineupRepository.CountStartersByMatchAndTeamAsync(matchId, player.TeamId);
            if (starters >= 11)
                throw new InvalidOperationException("El equipo ya tiene 11 titulares registrados en este partido");
        }

        lineup.MatchId = matchId;
        _logger.LogInformation("Registering lineup: Match {MatchId}, Player {PlayerId}", matchId, lineup.PlayerId);
        return await _matchLineupRepository.CreateAsync(lineup);
    }

    public async Task<IEnumerable<MatchLineup>> GetLineupByMatchAsync(int matchId)
    {
        var match = await _matchRepository.GetByIdasync(matchId)
            ?? throw new KeyNotFoundException($"No se encontró el partido con ID {matchId}");

        return await _matchLineupRepository.GetByMatchWithDetailsAsync(match.Id);
    }

    public async Task<IEnumerable<MatchLineup>> GetLineupByMatchAndTeamAsync(int matchId, int teamId)
    {
        var match = await _matchRepository.GetByIdasync(matchId)
            ?? throw new KeyNotFoundException($"No se encontró el partido con ID {matchId}");

        if (teamId != match.HomeTeamId && teamId != match.AwayTeamId)
            throw new InvalidOperationException("El equipo no pertenece al partido indicado");

        return await _matchLineupRepository.GetByMatchAndTeamWithDetailsAsync(matchId, teamId);
    }

    public async Task DeleteLineupPlayerAsync(int matchId, int lineupId)
    {
        var match = await _matchRepository.GetByIdasync(matchId)
            ?? throw new KeyNotFoundException($"No se encontró el partido con ID {matchId}");

        var lineup = await _matchLineupRepository.GetByIdasync(lineupId)
            ?? throw new KeyNotFoundException($"No se encontró el registro de alineación con ID {lineupId}");

        if (lineup.MatchId != match.Id)
            throw new InvalidOperationException("El registro de alineación no pertenece al partido indicado");

        await _matchLineupRepository.DeleteAsync(lineupId);
    }
}