using MyStore.Database;
using System.Data.Entity;

namespace MyStore.Database
{
  public interface IMyStoreEntities
  {
    DbSet<Product> Products { get; set; }
  }
}
