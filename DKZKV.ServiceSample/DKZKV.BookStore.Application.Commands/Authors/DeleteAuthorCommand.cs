using DKZKV.BookStore.Domain.AggregatesModel.Author;
using DKZKV.BookStore.Domain.Repositories;
using MediatR;

namespace DKZKV.BookStore.Application.Commands.Authors;

public class DeleteAuthorCommand : IRequest, ITransactionalCommand
{
    public DeleteAuthorCommand(Guid authorId)
    {
        AuthorId = authorId;
    }

    public Guid AuthorId { get; }

    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IBookStoreRepository<Author, Guid> _authorRepository;

        public DeleteAuthorCommandHandler(IBookStoreRepository<Author, Guid> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            await _authorRepository.DeleteAsync(request.AuthorId, cancellationToken);
            return Unit.Value;
        }
    }
}