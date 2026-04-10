using SportsLeague.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ITeamRepository : IGenericRepository<Teams>
    {
        Task<Teams?> GetByNameAsync(string name);
        Task<IEnumerable<Teams>> GetByCityAsync(string city);
    }
}
