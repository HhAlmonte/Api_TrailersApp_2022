namespace Core.Interface
{
    public interface IUtilitiesRepository<T> where T : class
    {
        Task<T> GetItemByIdAsync(string id);
        Task<IReadOnlyList<T>> GetAllAsync();
    }
}
