using DKZKV.BookStore.Domain.SeedWork;

namespace DKZKV.BookStore.Domain.AggregatesModel.Author;

public class Book : Entity<Guid>
{
    public Book(Guid authorId, string name, DateOnly wroteAt, BookStyle style)
    {
        Id = Guid.NewGuid();
        AuthorId = authorId;
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Book name is invalid");
        if (wroteAt.Equals(DateOnly.MinValue))
            throw new ArgumentException("Book date is not valid");

        Name = name;
        WroteAt = wroteAt;
        Style = style;
    }

    protected Book(Guid id, Guid authorId, string name, DateOnly wroteAt, BookStyle style)
    {
        Id = id;
        AuthorId = authorId;
        Name = name;
        WroteAt = wroteAt;
        Style = style;
    }

    public Guid AuthorId { get; }
    public string Name { get; }
    public DateOnly WroteAt { get; }
    public BookStyle Style { get; }
}