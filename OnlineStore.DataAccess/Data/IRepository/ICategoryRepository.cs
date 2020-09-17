using OnlineStore.Models;

namespace OnlineStore.DataAccess.Data.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
