using DKZKV.BookStore.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8618
namespace DKZKV.BookStore.Persistence;

public class BookStoreBdContext : DbContext
{
    public BookStoreBdContext(DbContextOptions<BookStoreBdContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookStoreBdContext).Assembly);
    }
}