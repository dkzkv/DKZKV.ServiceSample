using DKZKV.BookStore.Domain.AggregatesModel.Author;

namespace DKZKV.BookStore.Domain.Exceptions;

public class AlreadyExistedBookException : DomainException
{
    public AlreadyExistedBookException(Book book, Author author)
        : base("Already existed book", $"Author {author.LastName} {author.FirstName}, already has book: {book.Name}")
    {
    }
}