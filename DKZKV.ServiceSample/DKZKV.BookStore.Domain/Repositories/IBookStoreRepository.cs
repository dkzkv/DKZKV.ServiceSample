using DKZKV.BookStore.Domain.SeedWork;

namespace DKZKV.BookStore.Domain.Repositories;

public interface IBookStoreRepository<T, TKey> where T : IAggregateRoot<TKey>
{
    Task<T> GetAsync(TKey id, CancellationToken token = default);
    Task<TKey> CreateAsync(T book, CancellationToken token = default);
    Task UpdateAsync(T aggregate, CancellationToken token = default);
    Task DeleteAsync(TKey id, CancellationToken token = default);
}