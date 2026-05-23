using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsLeague.API.DTOs.Request;
using SportsLeague.API.DTOs.Response;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/match/{matchId}/lineup")]
public class MatchLineupController : ControllerBase
{
    private readonly IMatchLineupService _matchLineupService;
    private readonly IMapper _mapper;

    public MatchLineupController(IMatchLineupService matchLineupService, IMapper mapper)
    {
        _matchLineupService = matchLineupService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<MatchLineupResponseDto>> Create(int matchId, MatchLineupRequestDto dto)
    {
        try
        {
            var lineup = _mapper.Map<MatchLineup>(dto);
            var created = await _matchLineupService.RegisterLineupPlayerAsync(matchId, lineup);
            var list = await _matchLineupService.GetLineupByMatchAsync(matchId);
            var createdLineup = list.FirstOrDefault(x => x.Id == created.Id);
            return Ok(_mapper.Map<MatchLineupResponseDto>(createdLineup));
        }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MatchLineupResponseDto>>> GetMatch(int matchId)
    {
        try
        {
            var lineup = await _matchLineupService.GetLineupByMatchAsync(matchId);
            return Ok(_mapper.Map<IEnumerable<MatchLineupResponseDto>>(lineup));
        }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpGet("team/{teamId}")]
    public async Task<ActionResult<IEnumerable<MatchLineupResponseDto>>> GetByTeam(int matchId, int teamId)
    {
        try
        {
            var lineup = await _matchLineupService.GetLineupByMatchAndTeamAsync(matchId, teamId);
            return Ok(_mapper.Map<IEnumerable<MatchLineupResponseDto>>(lineup));
        }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int matchId, int id)
    {
        try
        {
            await _matchLineupService.DeleteLineupPlayerAsync(matchId, id);
            return NoContent();
        }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
    }
}