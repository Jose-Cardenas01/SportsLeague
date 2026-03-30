using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenereicRepository<T> where T : AuditBase
    {
        private readonly LeagueDbContext _context;
        public GenericRepository(LeagueDbContext context)
        {
            _context = context;
        }
        public async Task<T> CreateAsync(T entity)
        {
            if (entity != null)
            {
                entity.CreatedAt = DateTime.UtcNow;
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if(entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Set<T>().AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllasync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdasync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);            
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
