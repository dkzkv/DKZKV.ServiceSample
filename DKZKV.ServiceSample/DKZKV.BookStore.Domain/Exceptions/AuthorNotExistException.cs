namespace DKZKV.BookStore.Domain.Exceptions;

public class AuthorNotExistException : DomainObjectNoFountException
{
    public AuthorNotExistException(Guid authorId)
        : base("Author not exist", $"Author not exist with id: {authorId}")
    {
    }
}