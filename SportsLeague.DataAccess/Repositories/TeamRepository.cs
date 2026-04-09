using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.DataAccess.Repositories
{
    public class TeamRepository : GenericRepository<Teams>, ITeamRepository
    {
        public TeamRepository(LeagueDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Teams>> GetByCityAsync(string city)
        {
            return await _context.Set<Teams>().Where(t => t.City.ToLower() == city.ToLower()).ToListAsync();
        }

        public async Task<Teams?> GetByNameAsync(string name)
        {
            return await _context.Set<Teams>().FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
        }
    }
}
