using DKZKV.BookStore.Presentation.Models.Paging;
using Microsoft.AspNetCore.Mvc;

namespace DKZKV.BookStore.Presentation.Models;

/// <summary>
///     Book filter
/// </summary>
public class BookFilter : PageFilter
{
    /// <summary>
    ///     Filter by book names
    /// </summary>
    [FromQuery(Name = "bn")]
    public string? BookName { get; set; }

    /// <summary>
    ///     Filter by authors first name
    /// </summary>
    [FromQuery(Name = "afn")]
    public string? AuthorFirstName { get; set; }

    /// <summary>
    ///     Filter by authors last name
    /// </summary>
    [FromQuery(Name = "aln")]
    public string? AuthorLastName { get; set; }

    /// <summary>
    ///     Filter book witch were written after this date
    /// </summary>
    [FromQuery(Name = "wa")]
    public DateTime? WroteAfter { get; set; }

    /// <summary>
    ///     Filter book witch were written before this date
    /// </summary>
    [FromQuery(Name = "wb")]
    public DateTime? WroteBefore { get; set; }
}