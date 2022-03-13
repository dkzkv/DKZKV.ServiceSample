using AutoMapper;
using DKZKV.BookStore.Application.Queries.Books;
using DKZKV.BookStore.Application.Queries.QueryModels;
using DKZKV.BookStore.Domain.Exceptions;
using DKZKV.BookStore.Persistence.QueryHandlers.Specs;
using DKZKV.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DKZKV.BookStore.Persistence.QueryHandlers;

public class BookQueryHandler : BaseHandler, IRequestHandler<GetBooksByIdQuery, IBook>,
    IRequestHandler<GetBooksByFilterQuery, Page<IBookShortInfo>>
{
    public BookQueryHandler(BookStoreBdContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<Page<IBookShortInfo>> Handle(GetBooksByFilterQuery request, CancellationToken cancellationToken)
    {
        var a = await BooksQuery.Include(o => o.Author)
            .Where(BookSpecs.ByBookName(request.Filter.BookName))
            .Where(BookSpecs.ByAuthorFirstName(request.Filter.AuthorFirstName))
            .Where(BookSpecs.ByAuthorLastName(request.Filter.AuthorLastName))
            .Where(BookSpecs.GreaterThen(request.Filter.WroteAfter))
            .Where(BookSpecs.LessThen(request.Filter.WroteBefore))
            .OrderBy(o => o.Name).ToArrayAsync();

        return await BooksQuery.Include(o => o.Author)
            .Where(BookSpecs.ByBookName(request.Filter.BookName))
            .Where(BookSpecs.ByAuthorFirstName(request.Filter.AuthorFirstName))
            .Where(BookSpecs.ByAuthorLastName(request.Filter.AuthorLastName))
            .Where(BookSpecs.GreaterThen(request.Filter.WroteAfter))
            .Where(BookSpecs.LessThen(request.Filter.WroteBefore))
            .OrderBy(o => o.Name)
            .Page(request.Filter, Mapper.Map<IBookShortInfo>, cancellationToken);
    }

    public async Task<IBook> Handle(GetBooksByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await BooksQuery.FirstOrDefaultAsync(o => o.Id == request.BookId, cancellationToken);

        if (book is null)
            throw new BookNotExistException(request.BookId);

        return Mapper.Map<IBook>(book);
    }
}