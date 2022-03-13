using DKZKV.BookStore.Domain.AggregatesModel.Author;
using JetBrains.Annotations;

namespace DKZKV.BookStore.Persistence.Mapping.AuxiliaryModels;

internal class DomainBookAuxiliaryModel : Book
{
    public DomainBookAuxiliaryModel(Guid authorId, [NotNull] string name, DateOnly wroteAt, [NotNull] BookStyle style) :
        base(authorId, name, wroteAt, style)
    {
    }

    public DomainBookAuxiliaryModel(Guid id, Guid authorId, [NotNull] string name, DateOnly wroteAt, [NotNull] BookStyle style) :
        base(id, authorId, name, wroteAt, style)
    {
    }
}