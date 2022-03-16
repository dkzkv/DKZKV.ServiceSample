using DKZKV.BookStore.Application.Queries.QueryModels;
using MediatR;

namespace DKZKV.BookStore.Application.Queries.Books;

public class GetBooksByIdQuery : IRequest<IBook>
{
    public GetBooksByIdQuery(Guid bookId)
    {
        BookId = bookId;
    }

    public Guid BookId { get; }
}