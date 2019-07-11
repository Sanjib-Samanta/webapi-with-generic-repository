using MyStore.Database;

namespace MyStore.Repository.Interface
{
  public interface IDbFactory
  {
    MyStoreEntities Init();
  }
}
