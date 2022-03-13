namespace DKZKV.BookStore.Presentation.Models;

/// <summary>
///     Book
/// </summary>
public class Book
{
    /// <summary>
    ///     Book identifier
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    ///     Book name
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    ///     When book was written
    /// </summary>
    public DateTime WroteAt { get; init; }

    /// <summary>
    ///     Book style
    /// </summary>
    public string Style { get; init; } = null!;

    /// <summary>
    ///     Author who wrote this book
    /// </summary>
    public Author Author { get; init; } = null!;
}