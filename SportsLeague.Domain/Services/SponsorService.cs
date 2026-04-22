using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;
using System.Linq;

namespace SportsLeague.Domain.Services
{
    public class SponsorService : ISponsorService
    {
        private readonly ISponsorRepository _sponsorRepo;
        private readonly ITournamentSponsorRepository _tsRepo;
        private readonly ITournamentRepository _tournamentRepo;

        public SponsorService(ISponsorRepository sponsorRepo, ITournamentSponsorRepository tsRepo, ITournamentRepository tournamentRepo)
        {
            _sponsorRepo = sponsorRepo;
            _tsRepo = tsRepo;
            _tournamentRepo = tournamentRepo;
        }

        public async Task<Sponsors> CreateAsync(Sponsors sponsor)
        {
            if (await _sponsorRepo.ExistsByNameAsync(sponsor.Name))
            {
                throw new InvalidOperationException("nombre duplicado");
            }

            sponsor.CreatedAt = DateTime.UtcNow;
            return await _sponsorRepo.CreateAsync(sponsor);
        }

        public async Task<IEnumerable<Sponsors>> GetAllAsync()
        {
            return await _sponsorRepo.GetAllasync();
        }

        public async Task<Sponsors> GetByIdAsync(int id)
        {
            var sponsor = await _sponsorRepo.GetByIdasync(id);
            if (sponsor == null)
                throw new KeyNotFoundException("Sponsor no encontrado");

            return sponsor;
        }

        public async Task UpdateAsync(int id, Sponsors updated)
        {
            var sponsor = await _sponsorRepo.GetByIdasync(id);

            if (sponsor == null)
                throw new KeyNotFoundException("Sponsor no encontrado");

            sponsor.Name = updated.Name;
            sponsor.ContactEmail = updated.ContactEmail;
            sponsor.Phone = updated.Phone;
            sponsor.WebsiteUrl = updated.WebsiteUrl;
            sponsor.Category = updated.Category;
            sponsor.UpdatedAt = DateTime.UtcNow;

            await _sponsorRepo.UpdateAsync(sponsor);
        }

        public async Task DeleteAsync(int id)
        {
            var sponsor = await _sponsorRepo.GetByIdasync(id);

            if (sponsor == null)
                throw new KeyNotFoundException("Sponsor no encontrado");

            await _sponsorRepo.DeleteAsync(id);
        }

        public async Task<TournamentSponsor> LinkAsync(int sponsorId, TournamentSponsor entity)
        {
            var sponsor = await _sponsorRepo.GetByIdasync(sponsorId);
            if (sponsor == null)
            { 
                throw new KeyNotFoundException("Sponsor no existe");
            }
            if (entity == null)
            {
                throw new InvalidOperationException("Entidad no puede ser nula");
            }
            var tournament = await _tournamentRepo.GetByIdasync(entity.TournamentId);
            if (tournament == null)
            {
                throw new KeyNotFoundException("Tournament no existe");
            }
            if (await _tsRepo.ExistsAsync(entity.TournamentId, sponsorId))
            {
                throw new InvalidOperationException("Relación duplicada");
            }
            var newEntity = new TournamentSponsor
            {
                SponsorId = sponsorId,
                TournamentId = entity.TournamentId,
                ContractAmount = entity.ContractAmount,
                JoinedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                Sponsor = sponsor,
                Tournament = tournament
            };

            return await _tsRepo.CreateAsync(newEntity);
        }

        public async Task<IEnumerable<Tournament>> GetTournamentsAsync(int sponsorId)
        {
            return await _tsRepo.GetOnlyTournamentsIdAsync(sponsorId);
        }

        public async Task UnbindingAsync(int sponsorId, int tournamentId)
        {
            var tournamentSponsors = await _tsRepo.GetAllasync();

            var entity = tournamentSponsors.FirstOrDefault(x =>
                x.SponsorId == sponsorId &&
                x.TournamentId == tournamentId);

            if (entity == null)
                throw new KeyNotFoundException("Relación no existe");

            await _tsRepo.DeleteAsync(entity.Id);
        }

    }
}
