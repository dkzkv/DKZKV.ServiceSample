using DKZKV.BookStore.Application.Queries.QueryModels;
using DKZKV.BookStore.Domain.AggregatesModel.Author;

#pragma warning disable CS8618
namespace DKZKV.BookStore.Persistence.Mapping.AuxiliaryModels;

internal class BookAuxiliaryModel : IBook
{
    public Guid Id { get; set; }
    public string BookName { get; set; }
    public DateTime WroteAt { get; set; }
    public BookStyle Style { get; set; }
    public Guid AuthorId { get; set; }
}