using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface ITeamServices //VALIDACIONES ANTES DE APLICAR LOS SERVICIOS DE REPOSITORIOS
    {
        Task<IEnumerable<Teams>> GetAllAsync();
        Task<Teams?> GetByIdAsync(int id);
        Task<Teams> CreateAsync(Teams team);
        Task UpdateAsync(int id, Teams team);
        Task DeleteAsync(int id);
    }
}
