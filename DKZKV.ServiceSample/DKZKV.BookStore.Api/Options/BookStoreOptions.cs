using DKZKV.MandatoryOptions;

namespace DKZKV.BookStore.Options;

[MandatoryOptions(nameof(BookStoreOptions))]
internal class BookStoreOptions
{
    public string DatabaseConnection { get; set; } = null!;
}