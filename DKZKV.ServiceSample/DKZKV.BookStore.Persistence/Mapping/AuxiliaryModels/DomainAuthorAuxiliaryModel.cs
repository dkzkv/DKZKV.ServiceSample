using DKZKV.BookStore.Domain.AggregatesModel.Author;
using JetBrains.Annotations;

namespace DKZKV.BookStore.Persistence.Mapping.AuxiliaryModels;

internal class DomainAuthorAuxiliaryModel : Author
{
    public DomainAuthorAuxiliaryModel([NotNull] string firstName, [NotNull] string lastName, DateOnly birthDate) : base(firstName, lastName, birthDate)
    {
    }

    public DomainAuthorAuxiliaryModel(Guid id, [NotNull] string firstName, [NotNull] string lastName, DateOnly birthDate, DateOnly? deathDate,
        [NotNull] [ItemNotNull] IEnumerable<Book> books) : base(id, firstName, lastName, birthDate, deathDate, books)
    {
    }
}