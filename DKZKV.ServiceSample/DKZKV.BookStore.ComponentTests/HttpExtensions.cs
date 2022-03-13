using System;
using System.Threading.Tasks;
using DKZKV.BookStore.Presentation.Models;
using DKZKV.BookStore.Presentation.Models.Paging;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;

namespace DKZKV.BookStore.ComponentTests;

public static class HttpExtensions
{
    private static Task<T> GetInnerAuthorById<T>(this FlurlClient flurlClient, Guid id)
    {
        return flurlClient
            .AllowAnyHttpStatus()
            .Request($"/api/v1/author/{id}")
            .GetAsync()
            .ReceiveJson<T>();
    }

    private static Task<T> GetInnerBookById<T>(this FlurlClient flurlClient, Guid id)
    {
        return flurlClient
            .AllowAnyHttpStatus()
            .Request($"/api/v1/book/{id}")
            .GetAsync()
            .ReceiveJson<T>();
    }

    public static Task<Author> GetAuthorById(this FlurlClient flurlClient, Guid id)
    {
        return flurlClient.GetInnerAuthorById<Author>(id);
    }

    public static Task<ProblemDetails> GetErrorAuthorById(this FlurlClient flurlClient, Guid id)
    {
        return flurlClient.GetInnerAuthorById<ProblemDetails>(id);
    }

    public static Task<Book> GetBookById(this FlurlClient flurlClient, Guid id)
    {
        return flurlClient.GetInnerBookById<Book>(id);
    }

    public static Task<ProblemDetails> GetErrorBookById(this FlurlClient flurlClient, Guid id)
    {
        return flurlClient.GetInnerBookById<ProblemDetails>(id);
    }

    public static async Task<Page<BookShortInfo>> GetBookByFilter(this FlurlClient flurlClient, BookFilter? filter = null)
    {
        var resp = await flurlClient
            .AllowAnyHttpStatus()
            .Request("/api/v1/book")
            .SetQueryParam("bn", filter?.BookName)
            .SetQueryParam("afn", filter?.AuthorFirstName)
            .SetQueryParam("aln", filter?.AuthorLastName)
            .SetQueryParam("wa", filter?.WroteAfter)
            .SetQueryParam("wb", filter?.WroteBefore)
            .GetJsonAsync<Page<BookShortInfo>>();
        return resp;
    }

    public static Task<Guid> CreateAuthor(this FlurlClient flurlClient, AuthorRegistration authorRegistration)
    {
        return flurlClient.AllowAnyHttpStatus()
            .Request("/api/v1/author")
            .PostJsonAsync(authorRegistration)
            .ReceiveJson<Guid>();
    }

    public static Task<Guid> AddBook(this FlurlClient flurlClient, Guid authorId, BookRegistration bookRegistration)
    {
        return flurlClient.AllowAnyHttpStatus()
            .Request($"/api/v1/author/{authorId}/book")
            .PostJsonAsync(bookRegistration)
            .ReceiveJson<Guid>();
    }
}