using AutoMapper;
using DKZKV.BookStore.Domain.AggregatesModel.Author;
using DKZKV.BookStore.Domain.Exceptions;
using DKZKV.BookStore.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Book = DKZKV.BookStore.Persistence.Entities.Book;

namespace DKZKV.BookStore.Persistence.Repositories;

public class AuthorRepository : BaseRepository, IBookStoreRepository<Author, Guid>
{
    public AuthorRepository(IMapper mapper, BookStoreBdContext dbContext) : base(mapper, dbContext)
    {
    }

    public async Task<Author> GetAsync(Guid id, CancellationToken token = default)
    {
        var author = await DbContext.Authors
            .Include(o => o.Books.Where(o => !o.DeletedAtUtc.HasValue))
            .FirstOrDefaultAsync(o => o.Id.Equals(id) && !o.DeletedAtUtc.HasValue, token);
        if (author is null)
            throw new AuthorNotExistException(id);
        return Mapper.Map<Author>(author);
    }

    public async Task<Guid> CreateAsync(Author author, CancellationToken token = default)
    {
        await DbContext.Authors.AddAsync(Mapper.Map<Entities.Author>(author), token);
        await DbContext.SaveChangesAsync(token);
        return author.Id;
    }

    public async Task UpdateAsync(Author author, CancellationToken token = default)
    {
        var existedAuthor = await DbContext.Authors
            .Include(o => o.Books.Where(o => !o.DeletedAtUtc.HasValue))
            .FirstOrDefaultAsync(o => o.Id.Equals(author.Id) && !o.DeletedAtUtc.HasValue, token);
        if (existedAuthor is null)
            throw new AuthorNotExistException(author.Id);
        existedAuthor.FirstName = author.FirstName;
        existedAuthor.LastName = author.LastName;
        existedAuthor.BirthDate = author.BirthDate.ToDateTime(new TimeOnly());
        existedAuthor.DeathDate = author.DeathDate?.ToDateTime(new TimeOnly());

        var updatedEntityBooks = Mapper.Map<ICollection<Book>>(author.Books).ToDictionary(o => o.Id);
        foreach (var book in existedAuthor.Books)
            if (!updatedEntityBooks[book.Id].Name.Equals(book.Name))
                book.Name = updatedEntityBooks[book.Id].Name;

        var newBooks = updatedEntityBooks.Select(o => o.Key).Except(existedAuthor.Books.Select(o => o.Id));
        foreach (var newBookId in newBooks) existedAuthor.Books.Add(updatedEntityBooks[newBookId]);
        var deletedBooks = existedAuthor.Books.Select(o => o.Id).Except(updatedEntityBooks.Select(o => o.Key));
        foreach (var deletedBookId in deletedBooks)
        {
            var deletedBook = existedAuthor.Books.First(o => o.Id == deletedBookId);
            deletedBook.DeletedAtUtc = DateTime.UtcNow;
        }

        await DbContext.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(Guid id, CancellationToken token = default)
    {
        var existedAuthor = await DbContext.Authors.FirstOrDefaultAsync(o => o.Id.Equals(id), token);
        if (existedAuthor is not null && !existedAuthor.DeletedAtUtc.HasValue)
        {
            existedAuthor.DeletedAtUtc = DateTime.Now.ToUniversalTime();
            await DbContext.SaveChangesAsync(token);
        }
    }
}