using System;
using System.Linq;
using System.Threading.Tasks;
using DKZKV.BookStore.ComponentTests.Infrastructure;
using DKZKV.BookStore.ComponentTests.Infrastructure.DataUploader;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Shouldly;
using Xunit;

namespace DKZKV.BookStore.ComponentTests;

public class PredefinedDataTests : IClassFixture<BookStoreWithDataWebApplicationFactory>
{
    private readonly BookStoreWithDataWebApplicationFactory _factory;

    public PredefinedDataTests(BookStoreWithDataWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact(DisplayName = "Get valid author by id")]
    public async Task GetAuthor()
    {
        //Arrange
        var flurlClient = new FlurlClient(_factory.CreateClient());

        //Act
        var author = await flurlClient.GetAuthorById(AuthorsAndBooksUploader.AuthorWithoutBooksId);

        //Assert
        author.ShouldNotBeNull();
        author.Id.ShouldBe(AuthorsAndBooksUploader.AuthorWithoutBooksId);
        author.DeathDate.ShouldBeNull();
        author.FirstName.ShouldBe("Author's_first_name_1");
        author.LastName.ShouldBe("Author's_second_name_1");
    }

    [Fact(DisplayName = "Get unreal author by id")]
    public async Task TryGetUnrealAuthor()
    {
        //Arrange
        var flurlClient = new FlurlClient(_factory.CreateClient());

        //Act
        var problemDetails = await flurlClient.GetErrorAuthorById(Guid.NewGuid());

        //Assert
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact(DisplayName = "Get deleted author by id")]
    public async Task TryGetDeletedAuthor()
    {
        //Arrange
        var flurlClient = new FlurlClient(_factory.CreateClient());

        //Act
        var problemDetails = await flurlClient.GetErrorAuthorById(AuthorsAndBooksUploader.DeletedAuthorWithoutBooksId);

        //Assert
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact(DisplayName = "Get book by id")]
    public async Task GetBook()
    {
        //Arrange
        var flurlClient = new FlurlClient(_factory.CreateClient());

        //Act
        var book = await flurlClient.GetBookById(AuthorsAndBooksUploader.BookId);

        //Assert
        book.ShouldNotBeNull();
        book.Name.ShouldBe("Some_book_1");
        book.Author.ShouldNotBeNull();
        book.Author.FirstName.ShouldBe("Author's_first_name_3");
    }

    [Fact(DisplayName = "Get unreal books by id")]
    public async Task TryGetUnrealBook()
    {
        //Arrange
        var flurlClient = new FlurlClient(_factory.CreateClient());

        //Act
        var problemDetails = await flurlClient.GetErrorBookById(Guid.NewGuid());

        //Assert
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact(DisplayName = "Get books by empty filter")]
    public async Task GetBooksByEmptyFilter()
    {
        //Arrange
        var flurlClient = new FlurlClient(_factory.CreateClient());

        //Act
        var books = await flurlClient.GetBookByFilter();

        //Assert
        books.Total.ShouldBe(2);
        books.Items.ShouldNotBeNull();
        books.Items.Count().ShouldBe(2);
    }
}