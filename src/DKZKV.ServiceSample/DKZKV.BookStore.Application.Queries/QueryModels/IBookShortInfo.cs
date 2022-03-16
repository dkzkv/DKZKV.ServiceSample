namespace DKZKV.BookStore.Application.Queries.QueryModels;

public interface IBookShortInfo
{
    public Guid Id { get; }
    public string BookName { get; }
    public Guid AuthorId { get; }
    public string AuthorFirstName { get; }
    public string AuthorLastName { get; }
}