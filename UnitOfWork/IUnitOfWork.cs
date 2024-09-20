using ReportesCabildoAwa.Repository;

namespace ReportesCabildoAwa.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> Save();

    }
}
