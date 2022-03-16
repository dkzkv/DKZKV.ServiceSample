using AutoMapper;
using DKZKV.BookStore.Application.Queries.Authors;
using DKZKV.BookStore.Application.Queries.QueryModels;
using DKZKV.BookStore.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DKZKV.BookStore.Persistence.QueryHandlers;

public class AuthorQueryHandler : BaseHandler, IRequestHandler<GetAuthorByIdQuery, IAuthor>
{
    public AuthorQueryHandler(BookStoreBdContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<IAuthor> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await AuthorsQuery.FirstOrDefaultAsync(o => o.Id == request.AuthorId, cancellationToken);

        if (author is null)
            throw new AuthorNotExistException(request.AuthorId);

        return Mapper.Map<IAuthor>(author);
    }
}