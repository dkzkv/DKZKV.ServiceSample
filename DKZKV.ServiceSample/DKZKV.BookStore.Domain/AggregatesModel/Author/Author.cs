using DKZKV.BookStore.Domain.Exceptions;
using DKZKV.BookStore.Domain.SeedWork;

namespace DKZKV.BookStore.Domain.AggregatesModel.Author;

public class Author : Entity<Guid>, IAggregateRoot<Guid>
{
    private readonly List<Book> _books;

    public Author(string firstName, string lastName, DateOnly birthDate)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            throw new ArgumentException("Author first or second name are invalid");
        if (birthDate.Equals(DateOnly.MinValue))
            throw new ArgumentException("Author birth date is not valid");
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        _books = new List<Book>();
    }

    protected Author(Guid id, string firstName, string lastName, DateOnly birthDate, DateOnly? deathDate, IEnumerable<Book> books)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        DeathDate = deathDate;
        _books = books.ToList();
    }

    public IReadOnlyCollection<Book> Books => _books;

    public string FirstName { get; }
    public string LastName { get; }
    public DateOnly BirthDate { get; }
    public DateOnly? DeathDate { get; private set; }

    public bool IsDead => DeathDate.HasValue;

    public Guid AddNewBook(Book book)
    {
        if (book.AuthorId != Id)
            throw new ArgumentException("Wrong authors id");

        if (_books.Any(o => o.Name == book.Name))
            throw new AlreadyExistedBookException(book, this);
        _books.Add(book);
        return book.Id;
    }

    public Author RemoveBook(Guid bookId)
    {
        var existedBook = _books.FirstOrDefault(o => o.Id == bookId);
        if (existedBook is null)
            throw new BookNotExistException(bookId, this);
        _books.Remove(existedBook);
        return this;
    }

    public Author SetAsDead(DateOnly deathDate)
    {
        DeathDate = deathDate;
        return this;
    }
}