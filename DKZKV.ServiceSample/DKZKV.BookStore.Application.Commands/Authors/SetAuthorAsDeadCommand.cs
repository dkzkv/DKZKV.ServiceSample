using DKZKV.BookStore.Domain.AggregatesModel.Author;
using DKZKV.BookStore.Domain.Repositories;
using MediatR;

namespace DKZKV.BookStore.Application.Commands.Authors;

public class SetAuthorAsDeadCommand : IRequest, ITransactionalCommand
{
    public SetAuthorAsDeadCommand(Guid id, DateOnly deathDate)
    {
        Id = id;
        DeathDate = deathDate;
    }

    public Guid Id { get; }
    public DateOnly DeathDate { get; }

    public class SetAuthorAsDeadHandler : IRequestHandler<SetAuthorAsDeadCommand>
    {
        private readonly IBookStoreRepository<Author, Guid> _authorRepository;

        public SetAuthorAsDeadHandler(IBookStoreRepository<Author, Guid> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Unit> Handle(SetAuthorAsDeadCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAsync(request.Id, cancellationToken);
            author.SetAsDead(request.DeathDate);
            await _authorRepository.UpdateAsync(author, cancellationToken);
            return Unit.Value;
        }
    }
}