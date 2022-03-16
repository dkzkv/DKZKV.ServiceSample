using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DKZKV.BookStore.Persistence.Migrations;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BookStoreBdContext>
{
    public BookStoreBdContext CreateDbContext(string[] args)
    {
        return new BookStoreBdContext(new DbContextOptionsBuilder<BookStoreBdContext>()
            .UseSqlServer("connectionStringHere")
            .Options);
    }
}