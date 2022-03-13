namespace DKZKV.BookStore.Domain.SeedWork;

public abstract class Entity<TKey>
{
    public TKey Id { get; protected set; }
}