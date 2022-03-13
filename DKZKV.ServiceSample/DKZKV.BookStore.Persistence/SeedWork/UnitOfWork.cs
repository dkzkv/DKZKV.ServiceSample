using System.Data;
using DKZKV.BookStore.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DKZKV.BookStore.Persistence.SeedWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly BookStoreBdContext _dbContext;

    public UnitOfWorkFactory(BookStoreBdContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IUnitOfWork> CreateAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken token = default)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync(isolationLevel, token);
        return new UnitOfWork(transaction);
    }
}

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbContextTransaction _contextTransaction;

    public UnitOfWork(IDbContextTransaction contextTransaction)
    {
        _contextTransaction = contextTransaction;
    }

    public async Task CommitAsync(CancellationToken token = default)
    {
        await _contextTransaction.CommitAsync(token);
    }

    public async Task RollbackAsync(CancellationToken token = default)
    {
        await _contextTransaction.RollbackAsync(token);
    }

    public void Dispose()
    {
        _contextTransaction.Dispose();
    }
}