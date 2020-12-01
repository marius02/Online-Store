using OnlineStore.DataAccess.Data.IRepository;

namespace OnlineStore.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(db);
            Product = new ProductRepository(db);
            SpCall = new SP_Call(db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public ICategoryRepository Category { get; }
        public IProductRepository Product { get; }
        public ISP_Call SpCall { get; }
    }
}
