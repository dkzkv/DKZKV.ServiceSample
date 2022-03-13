namespace DKZKV.BookStore.Domain.Exceptions;

public class DomainObjectNoFountException : DomainException
{
    public DomainObjectNoFountException(string title, string description) : base(title, description)
    {
    }
}