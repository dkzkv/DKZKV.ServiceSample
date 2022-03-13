using DKZKV.BookStore.Domain.SeedWork;

namespace DKZKV.BookStore.Domain.AggregatesModel.Author;

public class BookStyle : Enumeration
{
    public static BookStyle Romance = new(1, "Romance");
    public static BookStyle Comedy = new(2, "Comedy");
    public static BookStyle Horror = new(3, "Horror");

    public BookStyle(int id, string name)
        : base(id, name)
    {
    }
}