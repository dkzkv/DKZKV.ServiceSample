using DKZKV.Paging;

namespace DKZKV.BookStore.Application.Queries.Books;

public class BookFilter : PageFilter
{
    public string? BookName { get; set; }
    public string? AuthorFirstName { get; set; }
    public string? AuthorLastName { get; set; }
    public DateTime? WroteAfter { get; set; }
    public DateTime? WroteBefore { get; set; }
}