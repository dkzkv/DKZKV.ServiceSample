using AutoMapper;
using DKZKV.BookStore.Application.Queries.QueryModels;
using DKZKV.Paging;

namespace DKZKV.BookStore.Presentation.Models.Mapping;

/// <inheritdoc />
public class PresentationToApplicationProfile : Profile
{
    /// <inheritdoc />
    public PresentationToApplicationProfile()
    {
        CreateMap(typeof(Page<>), typeof(Paging.Page<>));
        CreateMap<IAuthor, Author>();
        CreateMap<IBookShortInfo, BookShortInfo>();
    }
}