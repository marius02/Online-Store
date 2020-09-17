using System;

namespace OnlineStore.DataAccess.Data.IRepository
{
    public interface  IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        ISP_Call SpCall { get; }
    }
}
