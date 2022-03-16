using Newtonsoft.Json;

namespace DKZKV.BookStore.Presentation.Models;

/// <summary>
///     Information for book registration
/// </summary>
public class BookRegistration
{
    /// <summary>
    ///     Book name
    /// </summary>
    [JsonProperty(PropertyName = "book_name")]
    public string BookName { get; set; } = null!;

    /// <summary>
    ///     When book was written
    /// </summary>
    [JsonProperty(PropertyName = "wrote_at")]
    public DateTime WroteAt { get; set; }

    /// <summary>
    ///     Book style
    /// </summary>
    [JsonProperty(PropertyName = "style")]
    public string Style { get; set; } = null!;
}