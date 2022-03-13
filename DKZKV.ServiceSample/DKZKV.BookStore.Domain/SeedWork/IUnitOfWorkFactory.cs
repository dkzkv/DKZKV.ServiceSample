using System.Data;

namespace DKZKV.BookStore.Domain.SeedWork;

public interface IUnitOfWorkFactory
{
    Task<IUnitOfWork> CreateAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken token = default);
}