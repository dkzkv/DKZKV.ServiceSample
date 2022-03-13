using Newtonsoft.Json;

#pragma warning disable CS8618
namespace DKZKV.BookStore.Presentation.Models;

/// <summary>
///     Author's full information
/// </summary>
public class Author
{
    /// <summary>
    ///     Identifier
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; }

    /// <summary>
    ///     Author's first name
    /// </summary>
    [JsonProperty(PropertyName = "first_name")]
    public string FirstName { get; set; }

    /// <summary>
    ///     Author's second name
    /// </summary>
    [JsonProperty(PropertyName = "second_name")]
    public string LastName { get; set; }

    /// <summary>
    ///     Date of birth
    /// </summary>
    [JsonProperty(PropertyName = "birth_date")]
    public DateTime BirthDate { get; set; }

    /// <summary>
    ///     Date of death
    /// </summary>
    [JsonProperty(PropertyName = "death_name")]
    public DateTime? DeathDate { get; set; }
}