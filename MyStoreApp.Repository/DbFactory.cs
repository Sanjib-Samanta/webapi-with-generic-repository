using MyStore.Database;
using MyStore.Repository.Interface;

namespace MyStoreApp.Repository
{
  public class DbFactory : IDbFactory
  {
    MyStoreEntities _myStoreDbContext;
    public MyStoreEntities Init()
    {
      return _myStoreDbContext ?? (_myStoreDbContext = new MyStoreEntities());
    }
  }
}
