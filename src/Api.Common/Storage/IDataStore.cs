using System.Threading.Tasks;

namespace Api.Common
{
    /// <summary>
    /// Stores and manages data objects, where key is a string and the value is an object.
    /// </summary>
    public interface IDataStore
    {
        Task ClearAsync();
        Task DeleteAsync<T>(string key);
        Task<T> GetAsync<T>(string key);
        Task StoreAsync<T>(string key, T value);
    }
}
