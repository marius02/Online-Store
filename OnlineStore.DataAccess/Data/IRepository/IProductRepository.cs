using OnlineStore.Models;

namespace OnlineStore.DataAccess.Data.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
