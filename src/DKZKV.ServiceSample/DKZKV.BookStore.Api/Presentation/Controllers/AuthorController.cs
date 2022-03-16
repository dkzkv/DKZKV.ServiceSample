using AutoMapper;
using DKZKV.BookStore.Application.Commands.Authors;
using DKZKV.BookStore.Application.Commands.Books;
using DKZKV.BookStore.Application.Queries.Authors;
using DKZKV.BookStore.Domain.AggregatesModel.Author;
using DKZKV.BookStore.Domain.SeedWork;
using DKZKV.BookStore.Presentation.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Author = DKZKV.BookStore.Presentation.Models.Author;

namespace DKZKV.BookStore.Presentation.Controllers;

/// <summary>
///     Books controller
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    /// <inheritdoc />
    public AuthorController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    ///     Get concrete book by identifier
    /// </summary>
    /// <param name="authorId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{authorId}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById([FromRoute] Guid authorId, CancellationToken cancellationToken = default)
    {
        var author = await _mediator.Send(new GetAuthorByIdQuery(authorId), cancellationToken);
        return Ok(_mapper.Map<Author>(author));
    }

    /// <summary>
    ///     Register new author
    /// </summary>
    /// <param name="author"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] AuthorRegistration author, CancellationToken cancellationToken = default)
    {
        var authorId = await _mediator.Send(new CreateNewAuthorCommand(author.FirstName,
                author.LastName,
                new DateOnly(author.BirthDate.Year, author.BirthDate.Month, author.BirthDate.Day)),
            cancellationToken);

        return Ok(authorId);
    }

    /// <summary>
    ///     What a pity, author is dead
    /// </summary>
    /// <param name="authorId"></param>
    /// <param name="deathDate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("{authorId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetAsDead([FromRoute] Guid authorId, [FromBody] DateTime deathDate, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new SetAuthorAsDeadCommand(authorId, new DateOnly(deathDate.Day, deathDate.Month, deathDate.Day)), cancellationToken);
        return Ok();
    }

    /// <summary>
    ///     Add book to the author
    /// </summary>
    /// <param name="authorId"></param>
    /// <param name="bookRegistration"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{authorId}/book")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddBook([FromRoute] Guid authorId, [FromBody] BookRegistration bookRegistration,
        CancellationToken cancellationToken = default)
    {
        var bookId = await _mediator.Send(new AddBookCommand(authorId,
                bookRegistration.BookName,
                new DateOnly(bookRegistration.WroteAt.Year, bookRegistration.WroteAt.Month, bookRegistration.WroteAt.Day),
                Enumeration.FromDisplayName<BookStyle>(bookRegistration.Style)),
            cancellationToken);

        return Ok(bookId);
    }

    /// <summary>
    ///     Delete book from author
    /// </summary>
    /// <param name="authorId"></param>
    /// <param name="bookId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{authorId}/book/{bookId}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook([FromRoute] Guid authorId, [FromRoute] Guid bookId, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteBookCommand(authorId, bookId), cancellationToken);
        return Ok();
    }

    /// <summary>
    ///     Delete author
    /// </summary>
    /// <param name="authorId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] Guid authorId, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteAuthorCommand(authorId), cancellationToken);
        return Ok();
    }
}