using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using DKZKV.BookStore.Domain.AggregatesModel.Author;
using DKZKV.BookStore.Persistence;
using Microsoft.Extensions.Hosting;
using Author = DKZKV.BookStore.Persistence.Entities.Author;
using Book = DKZKV.BookStore.Persistence.Entities.Book;

namespace DKZKV.BookStore.ComponentTests.Infrastructure.DataUploader;

public class AuthorsAndBooksUploader : IHostedService
{
    public static Guid AuthorWithoutBooksId = Guid.NewGuid();
    public static Guid DeadAuthorWithoutBooksId = Guid.NewGuid();
    public static Guid AuthorWithBooksId = Guid.NewGuid();
    public static Guid DeletedAuthorWithoutBooksId = Guid.NewGuid();

    public static Guid BookId = Guid.NewGuid();
    public static Guid DeletedBookId = Guid.NewGuid();
    private readonly BookStoreBdContext _dBdContext;

    public AuthorsAndBooksUploader(BookStoreBdContext dBdContext)
    {
        _dBdContext = dBdContext;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var deletedAuthor = new Author
        {
            Id = DeletedAuthorWithoutBooksId,
            BirthDate = new DateTime(1992, 1, 1),
            Books = ImmutableArray<Book>.Empty,
            DeathDate = null,
            DeletedAtUtc = DateTime.Now,
            FirstName = "Author's_first_name_deleted",
            LastName = "Author's_second_name_deleted"
        };

        var authorWithoutBooks = new Author
        {
            Id = AuthorWithoutBooksId,
            BirthDate = new DateTime(1992, 1, 1),
            Books = ImmutableArray<Book>.Empty,
            DeathDate = null,
            DeletedAtUtc = null,
            FirstName = "Author's_first_name_1",
            LastName = "Author's_second_name_1"
        };

        var deadAuthorWithoutBooks = new Author
        {
            Id = DeadAuthorWithoutBooksId,
            BirthDate = new DateTime(1990, 1, 1),
            Books = ImmutableArray<Book>.Empty,
            DeathDate = new DateTime(2022, 02, 24),
            DeletedAtUtc = null,
            FirstName = "Author's_first_name_2",
            LastName = "Author's_second_name_2"
        };

        var authorWithBooks = new Author
        {
            Id = AuthorWithBooksId,
            BirthDate = new DateTime(1992, 1, 1),
            Books = new[]
            {
                new()
                {
                    Id = BookId,
                    AuthorId = AuthorWithBooksId,
                    DeletedAtUtc = null,
                    Name = "Some_book_1",
                    Style = (short)BookStyle.Horror.Id,
                    WroteAt = new DateTime(2000, 1, 1)
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    AuthorId = AuthorWithBooksId,
                    DeletedAtUtc = null,
                    Name = "Some_book_2",
                    Style = (short)BookStyle.Horror.Id,
                    WroteAt = new DateTime(2001, 1, 1)
                },
                new Book
                {
                    Id = DeletedBookId,
                    AuthorId = AuthorWithBooksId,
                    DeletedAtUtc = DateTime.Now,
                    Name = "Some_book_deleted_1",
                    Style = (short)BookStyle.Horror.Id,
                    WroteAt = new DateTime(2014, 1, 1)
                }
            },
            DeathDate = null,
            DeletedAtUtc = null,
            FirstName = "Author's_first_name_3",
            LastName = "Author's_second_name_3"
        };
        await _dBdContext.Authors.AddRangeAsync(new[] { deletedAuthor, authorWithoutBooks, deadAuthorWithoutBooks, authorWithBooks }, cancellationToken);
        await _dBdContext.SaveChangesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}