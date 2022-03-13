using System;
using System.Threading.Tasks;
using DKZKV.BookStore.ComponentTests.Infrastructure;
using DKZKV.BookStore.Presentation.Models;
using Flurl.Http;
using Shouldly;
using Xunit;

namespace DKZKV.BookStore.ComponentTests;

public class CleanSystemTests : IClassFixture<DefaultWebApplicationFactory>
{
    private readonly DefaultWebApplicationFactory _factory;

    public CleanSystemTests(DefaultWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact(DisplayName = "Create an author and add a book to him")]
    public async Task CreateAuthorAndAddBook()
    {
        //Arrange
        var flurlClient = new FlurlClient(_factory.CreateClient());
        var author = new AuthorRegistration
        {
            FirstName = "SomeFirstName",
            LastName = "SomeLastName",
            BirthDate = new DateTime(1992, 1, 1)
        };

        var book = new BookRegistration
        {
            BookName = "SomeBookName",
            Style = "Romance",
            WroteAt = DateTime.Now
        };

        //Act
        var authorId = await flurlClient.CreateAuthor(author);
        var bookId = await flurlClient.AddBook(authorId, book);
        var savedBook = await flurlClient.GetBookById(bookId);

        //Assert
        savedBook.Name.ShouldBe(book.BookName);
        savedBook.Style.ShouldBe(book.Style);

        savedBook.Author.ShouldNotBeNull();
        savedBook.Author.FirstName.ShouldBe(author.FirstName);
    }
}