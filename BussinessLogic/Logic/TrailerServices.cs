using BussinessLogic.Data;
using Core.Entities;
using Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace BussinessLogic.Logic
{
    public class TrailerServices : ITrailerRepository
    {
        private readonly TrailerDbContext _context;

        public TrailerServices(TrailerDbContext context)
        {
            _context = context;
        }


        //Metodo asincrono para crear
        public async Task<TrailersEntities> AddTrailers(TrailersEntities trailer)
        {
            _context.Add(trailer);

            await _context.SaveChangesAsync();

            return trailer;
        }

        //Metodo asincrono para actualizar data
        public async Task<int> UpdateTrailers(TrailersEntities trailer)
        {
            _context.Set<TrailersEntities>().Attach(trailer);

            _context.Entry(trailer).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        //Metodo para eliminar data
        public async Task<int> DeleteTrailers(TrailersEntities trailer)
        {
            _context.Set<TrailersEntities>().Attach(trailer);

            _context.Remove(trailer).State = EntityState.Deleted;

            return await _context.SaveChangesAsync();
        }
    }
}
