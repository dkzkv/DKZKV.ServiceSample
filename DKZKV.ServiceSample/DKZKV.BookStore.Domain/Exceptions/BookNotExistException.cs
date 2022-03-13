using DKZKV.BookStore.Domain.AggregatesModel.Author;

namespace DKZKV.BookStore.Domain.Exceptions;

public class BookNotExistException : DomainObjectNoFountException
{
    private const string ExceptionTitle = "Book not exist";

    public BookNotExistException(Guid bookId, Author author)
        : base(ExceptionTitle, $"Author {author.LastName} {author.FirstName}, do not have book with id: {bookId}")
    {
    }

    public BookNotExistException(Guid bookId)
        : base(ExceptionTitle, $"Book not exist with id {bookId}")
    {
    }
}