using BussinessLogic.Data;
using Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace BussinessLogic.Logic.utilities
{
    public class TrailersUtilitiesGeneric<T> : IUtilitiesRepository<T> where T : class
    {
        TrailerDbContext _context;

        public TrailersUtilitiesGeneric(TrailerDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetItemByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}
