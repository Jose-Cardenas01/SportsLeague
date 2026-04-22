using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sportsleague.API.DTOs.Request;
using Sportsleague.API.DTOs.Response;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;

namespace Sportsleague.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SponsorController : ControllerBase
    {
        private readonly ISponsorService _service;
        private readonly IMapper _mapper;

        public SponsorController(ISponsorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sponsors = await _service.GetAllAsync();
            var response = _mapper.Map<IEnumerable<SponsorResponseDTO>>(sponsors);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var sponsor = await _service.GetByIdAsync(id);
                var response = _mapper.Map<SponsorResponseDTO>(sponsor);

                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(SponsorRequestDTO dto)
        {
            try
            {
                var entity = _mapper.Map<Sponsors>(dto);

                var created = await _service.CreateAsync(entity);

                var response = _mapper.Map<SponsorResponseDTO>(created);

                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SponsorRequestDTO dto)
        {
            try
            {
                var entity = _mapper.Map<Sponsors>(dto);

                await _service.UpdateAsync(id, entity);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/tournaments")]
        public async Task<IActionResult> GetTournaments(int id)
        {
            var torunament = await _service.GetTournamentsAsync(id);

            var response = _mapper.Map<IEnumerable<TournamentResponseDTO>>(torunament);

            return Ok(response);
        }

        [HttpPost("{id}/tournaments")]
        public async Task<IActionResult> Link(int id, TournamentSponsorRequestDTO dto)
        {
            try
            {
                var ent = _mapper.Map<TournamentSponsor>(dto);
                var entity = await _service.LinkAsync(id, ent);

                var response = _mapper.Map<TournamentSponsorResponseDTO>(entity);

                return Created("", response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}/tournaments/{tid}")]
        public async Task<IActionResult> Delete(int id, int tid)
        {
            try
            {
                await _service.UnbindingAsync(id, tid);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
