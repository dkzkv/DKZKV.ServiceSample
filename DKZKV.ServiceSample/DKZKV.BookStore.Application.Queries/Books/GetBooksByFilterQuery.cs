using DKZKV.BookStore.Application.Queries.QueryModels;
using DKZKV.Paging;
using MediatR;

namespace DKZKV.BookStore.Application.Queries.Books;

public class GetBooksByFilterQuery : IRequest<Page<IBookShortInfo>>
{
    public GetBooksByFilterQuery(BookFilter filter)
    {
        Filter = filter;
    }

    public BookFilter Filter { get; }
}