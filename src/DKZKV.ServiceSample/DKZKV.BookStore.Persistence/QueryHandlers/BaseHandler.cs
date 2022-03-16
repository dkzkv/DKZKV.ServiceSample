using AutoMapper;
using DKZKV.BookStore.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace DKZKV.BookStore.Persistence.QueryHandlers;

public abstract class BaseHandler
{
    protected BaseHandler(BookStoreBdContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    protected BookStoreBdContext Context { get; }
    protected IMapper Mapper { get; }

    protected IQueryable<Author> AuthorsQuery => Context.Authors.AsNoTracking().Where(o => !o.DeletedAtUtc.HasValue);
    protected IQueryable<Book> BooksQuery => Context.Books.AsNoTracking().Where(o => !o.DeletedAtUtc.HasValue);
}