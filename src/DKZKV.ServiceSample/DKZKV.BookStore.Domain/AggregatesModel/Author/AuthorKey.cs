using DKZKV.BookStore.Domain.SeedWork;

namespace DKZKV.BookStore.Domain.AggregatesModel.Author;

public sealed class AuthorKey : SimpleKey<AuthorKey>
{
    public AuthorKey(Guid value) : base(value)
    {
    }
}