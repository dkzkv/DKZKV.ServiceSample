using DKZKV.BookStore.Domain.AggregatesModel.Author;
using DKZKV.BookStore.Domain.Repositories;
using MediatR;

namespace DKZKV.BookStore.Application.Commands.Authors;

public class CreateNewAuthorCommand : IRequest<Guid>, ITransactionalCommand
{
    public CreateNewAuthorCommand(string firstName, string lastName, DateOnly birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public DateOnly BirthDate { get; }

    public class CreateNewAuthorHandler : IRequestHandler<CreateNewAuthorCommand, Guid>
    {
        private readonly IBookStoreRepository<Author, Guid> _authorRepository;

        public CreateNewAuthorHandler(IBookStoreRepository<Author, Guid> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public Task<Guid> Handle(CreateNewAuthorCommand request, CancellationToken cancellationToken)
        {
            return _authorRepository.CreateAsync(new Author(request.FirstName, request.LastName, request.BirthDate), cancellationToken);
        }
    }
}