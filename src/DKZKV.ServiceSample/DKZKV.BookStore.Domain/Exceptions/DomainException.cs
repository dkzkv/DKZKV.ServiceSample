namespace DKZKV.BookStore.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string title, string description)
        : base(description)
    {
        Title = title;
        Description = description;
    }

    public string Title { get; }
    public string Description { get; }
}