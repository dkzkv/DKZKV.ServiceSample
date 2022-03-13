using DKZKV.BookStore.Application.Queries.QueryModels;

#pragma warning disable CS8618
namespace DKZKV.BookStore.Persistence.Mapping.AuxiliaryModels;

internal class BookShortInfoAuxiliaryModel : IBookShortInfo
{
    public Guid Id { get; set; }
    public string BookName { get; set; }
    public Guid AuthorId { get; set; }
    public string AuthorFirstName { get; set; }
    public string AuthorLastName { get; set; }
}