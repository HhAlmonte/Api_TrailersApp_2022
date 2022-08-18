using Core.Entities;

namespace Core.Interface
{
    public interface ITrailerRepository
    {
        Task<TrailersEntities> AddTrailers(TrailersEntities trailer);

        Task<int> UpdateTrailers(TrailersEntities trailer);

        Task<int> DeleteTrailers(TrailersEntities trailer);
    }
}
