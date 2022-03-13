using DKZKV.BookStore.Application.Queries.QueryModels;
using JetBrains.Annotations;
using MediatR;

namespace DKZKV.BookStore.Application.Queries.Authors;

[UsedImplicitly]
public class GetAuthorByIdQuery : IRequest<IAuthor>
{
    public GetAuthorByIdQuery(Guid authorId)
    {
        AuthorId = authorId;
    }

    public Guid AuthorId { get; }
}