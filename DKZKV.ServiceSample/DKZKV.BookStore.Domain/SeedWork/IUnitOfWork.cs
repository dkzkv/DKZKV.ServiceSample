namespace DKZKV.BookStore.Domain.SeedWork;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync(CancellationToken token = default);
    Task RollbackAsync(CancellationToken token = default);
}