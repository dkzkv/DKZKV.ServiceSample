using System.Linq.Expressions;
using DKZKV.BookStore.Persistence.Entities;

namespace DKZKV.BookStore.Persistence.QueryHandlers.Specs;

internal static class BookSpecs
{
    public static Expression<Func<Book, bool>> ByBookName(string? bookName)
    {
        if (string.IsNullOrEmpty(bookName)) return x => true;

        return distr => distr.Name.ToLower().StartsWith(bookName.ToLower());
    }

    public static Expression<Func<Book, bool>> GreaterThen(DateTime? dateTime)
    {
        if (!dateTime.HasValue) return x => true;

        return distr => distr.WroteAt > dateTime.Value;
    }

    public static Expression<Func<Book, bool>> LessThen(DateTime? dateTime)
    {
        if (!dateTime.HasValue) return x => true;

        return distr => distr.WroteAt < dateTime.Value;
    }

    public static Expression<Func<Book, bool>> ByAuthorFirstName(string? name)
    {
        if (string.IsNullOrEmpty(name)) return x => true;

        return distr => distr.Author.FirstName.ToLower().StartsWith(name.ToLower());
    }

    public static Expression<Func<Book, bool>> ByAuthorLastName(string? name)
    {
        if (string.IsNullOrEmpty(name)) return x => true;

        return distr => distr.Author.FirstName.ToLower().StartsWith(name.ToLower());
    }
}