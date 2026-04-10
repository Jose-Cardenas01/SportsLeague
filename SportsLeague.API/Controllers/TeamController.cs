using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportsleague.API.DTOs.Request;
using Sportsleague.API.DTOs.Response;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;

namespace Sportsleague.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamServices   _teamService;
        private readonly IMapper _map;
        private readonly ILogger<TeamController> _logger;
        public TeamController(ITeamServices teamServices, IMapper map, ILogger<TeamController> logger)
        {
            _teamService = teamServices;
            _map = map;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamResponseDTO>>> GetAll()
        {
            var teams = await _teamService.GetAllAsync();
            var teamResposeDTO = _map.Map<IEnumerable<TeamResponseDTO>>(teams);
            return Ok(teamResposeDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamResponseDTO>> GetById(int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null)
            {
                return NotFound(new { message = $"Equipo con ID {id} no encontrado" });
            }
            var teamDto = _map.Map<TeamResponseDTO>(team);
            return Ok(teamDto);
        }

        [HttpPost]
        public async Task<ActionResult<TeamResponseDTO>> Create(TeamRequestDTO dto)
        {
            try
            {
                var team = _map.Map<Teams>(dto);
                var createdTeam = await _teamService.CreateAsync(team);
                var responseDto = _map.Map<TeamResponseDTO>(createdTeam);
                return CreatedAtAction(
                nameof(GetById),
                new { id = responseDto.Id },
                responseDto);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, TeamRequestDTO dto)
        {
            try
            {
                var team = _map.Map<Teams>(dto);
                await _teamService.UpdateAsync(id, team);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _teamService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
