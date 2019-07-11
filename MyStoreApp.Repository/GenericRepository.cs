using MyStore.Database;
using MyStore.Repository.Interface;
using System.Linq;

namespace MyStoreApp.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private MyStoreEntities _mystoreDbContext;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected MyStoreEntities DbContext
        {
            get { return _mystoreDbContext ?? (_mystoreDbContext = DbFactory.Init()); }
        }

        public GenericRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }
        public IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }

        public T GetById<TId>(TId id)
        {
            return DbContext.Set<T>().Find(id);
        }
        
        public void AddItem(T item)
        {
            DbContext.Set<T>().Add(item);
        }

        public void RemoveItem(T item)
        {
            DbContext.Set<T>().Remove(item);
        }

        public bool SaveChanges()
        {
            return DbContext.SaveChanges() > 0;
        }
    }
}
