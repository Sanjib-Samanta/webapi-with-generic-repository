using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAll();

        T GetById<TId>(TId id);

        void AddItem(T item);

        void RemoveItem(T item);

        bool SaveChanges();
    }
}
