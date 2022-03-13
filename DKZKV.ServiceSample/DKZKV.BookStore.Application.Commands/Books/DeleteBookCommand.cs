using DKZKV.BookStore.Domain.AggregatesModel.Author;
using DKZKV.BookStore.Domain.Repositories;
using MediatR;

namespace DKZKV.BookStore.Application.Commands.Books;

public class DeleteBookCommand : IRequest, ITransactionalCommand
{
    public DeleteBookCommand(Guid authorId, Guid bookId)
    {
        BookId = bookId;
        AuthorId = authorId;
    }

    public Guid AuthorId { get; }
    public Guid BookId { get; }

    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookStoreRepository<Author, Guid> _authorRepository;

        public DeleteBookHandler(IBookStoreRepository<Author, Guid> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAsync(request.AuthorId, cancellationToken);
            author.RemoveBook(request.BookId);
            await _authorRepository.UpdateAsync(author, cancellationToken);
            return Unit.Value;
        }
    }
}