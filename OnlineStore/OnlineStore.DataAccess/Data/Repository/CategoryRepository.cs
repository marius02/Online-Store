using System.Linq;
using OnlineStore.DataAccess.Data.IRepository;
using OnlineStore.Models;

namespace OnlineStore.DataAccess.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Category category)
        {
            var objFromDb = _dbContext.Categories.FirstOrDefault(s => s.Id == category.Id);
            if (objFromDb != null) objFromDb.Name = category.Name;
        }
    }
}
