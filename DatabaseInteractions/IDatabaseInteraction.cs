using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseInteractions
{
    public interface IDatabaseInteraction
    {
        Task<List<T>> FindAll<T>();
        Task Replace<T1, T2>(T1 objectToQueryOn, T2 updatedObject);
        Task Log<T>(T objectToStore);
        Task LogMany<T>(IEnumerable<T> objectsToStore);
    }
}