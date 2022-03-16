using Newtonsoft.Json;

namespace DKZKV.BookStore.Presentation.Models;

/// <summary>
///     Book short info
/// </summary>
public class BookShortInfo
{
    /// <summary>
    ///     Book id
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; init; }

    /// <summary>
    ///     Book name
    /// </summary>
    [JsonProperty(PropertyName = "book_name")]
    public string? BookName { get; init; }

    /// <summary>
    ///     Author's id
    /// </summary>
    [JsonProperty(PropertyName = "author_id")]
    public Guid AuthorId { get; init; }

    /// <summary>
    ///     Author's first name
    /// </summary>
    [JsonProperty(PropertyName = "author_first_name")]
    public string? AuthorFirstName { get; init; }

    /// <summary>
    ///     Author's second name
    /// </summary>
    [JsonProperty(PropertyName = "author_second_name")]
    public string? AuthorLastName { get; init; }
}