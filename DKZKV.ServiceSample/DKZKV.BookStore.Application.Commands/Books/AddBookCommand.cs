using DKZKV.BookStore.Domain.AggregatesModel.Author;
using DKZKV.BookStore.Domain.Repositories;
using MediatR;

namespace DKZKV.BookStore.Application.Commands.Books;

public class AddBookCommand : IRequest<Guid>, ITransactionalCommand
{
    public AddBookCommand(Guid authorId, string bookName, DateOnly wroteAt, BookStyle style)
    {
        AuthorId = authorId;
        BookName = bookName;
        WroteAt = wroteAt;
        Style = style;
    }

    public Guid AuthorId { get; }
    public string BookName { get; }
    public DateOnly WroteAt { get; }
    public BookStyle Style { get; }


    public class DeleteBookHandler : IRequestHandler<AddBookCommand, Guid>
    {
        private readonly IBookStoreRepository<Author, Guid> _authorRepository;

        public DeleteBookHandler(IBookStoreRepository<Author, Guid> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Guid> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAsync(request.AuthorId, cancellationToken);
            var bookId = author.AddNewBook(new Book(request.AuthorId, request.BookName, request.WroteAt, request.Style));
            await _authorRepository.UpdateAsync(author, cancellationToken);
            return bookId;
        }
    }
}