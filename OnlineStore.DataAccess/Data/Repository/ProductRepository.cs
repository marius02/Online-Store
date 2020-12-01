using System.Linq;
using OnlineStore.DataAccess.Data.IRepository;
using OnlineStore.Models;

namespace OnlineStore.DataAccess.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Product product)
        {
            var objFromDb = _dbContext.Products.FirstOrDefault(s => s.Id == product.Id);
            if (objFromDb != null) objFromDb.ImageUrl = product.ImageUrl;

            objFromDb.ISBN = product.ISBN;
            objFromDb.Price = product.Price;
            objFromDb.Price50 = product.Price50;
            objFromDb.ListPrice = product.ListPrice;
            objFromDb.Price100 = product.Price100;
            objFromDb.Title = product.Title;
            objFromDb.Description = product.Description;
            objFromDb.CategoryId = product.CategoryId;
            objFromDb.Author = product.Author;
        }
    }
}
