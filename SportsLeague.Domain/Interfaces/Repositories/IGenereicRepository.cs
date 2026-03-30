using SportsLeague.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface IGenereicRepository<T> where T : AuditBase
    {
        Task<IEnumerable<T>> GetAllasync();
        Task<T?> GetByIdasync(int id);
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id);
        Task<bool> ExistAsync(int id);
    }
}
