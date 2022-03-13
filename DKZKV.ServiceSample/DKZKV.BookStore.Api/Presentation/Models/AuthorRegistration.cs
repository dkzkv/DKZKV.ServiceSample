using Newtonsoft.Json;

namespace DKZKV.BookStore.Presentation.Models;

/// <summary>
///     Information for author registration
/// </summary>
public class AuthorRegistration
{
    /// <summary>
    ///     Author's first name
    /// </summary>
    [JsonProperty(PropertyName = "first_name")]
    public string FirstName { get; init; } = null!;

    /// <summary>
    ///     Author's second name
    /// </summary>
    [JsonProperty(PropertyName = "second_name")]
    public string LastName { get; init; } = null!;

    /// <summary>
    ///     Author's birth date
    /// </summary>
    [JsonProperty(PropertyName = "birth_date")]
    public DateTime BirthDate { get; init; }
}