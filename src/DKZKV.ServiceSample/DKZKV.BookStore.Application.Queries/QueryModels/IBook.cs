using DKZKV.BookStore.Domain.AggregatesModel.Author;

namespace DKZKV.BookStore.Application.Queries.QueryModels;

public interface IBook
{
    public Guid Id { get; }
    public string BookName { get; }
    public DateTime WroteAt { get; }
    public BookStyle Style { get; }

    public Guid AuthorId { get; }
}