using AutoMapper;
using DKZKV.BookStore.Application.Queries.Authors;
using DKZKV.BookStore.Presentation.Models;
using DKZKV.BookStore.Presentation.Models.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryModel = DKZKV.BookStore.Application.Queries.Books;

namespace DKZKV.BookStore.Presentation.Controllers;

/// <summary>
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BookController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    /// <inheritdoc />
    public BookController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    ///     Get concrete book by id
    /// </summary>
    /// <param name="bookId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{bookId}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid bookId, CancellationToken cancellationToken = default)
    {
        var book = await _mediator.Send(new QueryModel.GetBooksByIdQuery(bookId), cancellationToken);
        var author = await _mediator.Send(new GetAuthorByIdQuery(book.AuthorId), cancellationToken);

        return Ok(new Book
        {
            Id = book.Id,
            Name = book.BookName,
            WroteAt = book.WroteAt,
            Style = book.Style.Name,
            Author = _mapper.Map<Author>(author)
        });
    }

    /// <summary>
    ///     Get paginated books by filter
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(BookShortInfo), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByFilter([FromQuery] BookFilter filter, CancellationToken cancellationToken = default)
    {
        var booksInfo = await _mediator.Send(new QueryModel.GetBooksByFilterQuery(_mapper.Map<QueryModel.BookFilter>(filter)), cancellationToken);
        return Ok(_mapper.Map<Page<BookShortInfo>>(booksInfo));
    }
}