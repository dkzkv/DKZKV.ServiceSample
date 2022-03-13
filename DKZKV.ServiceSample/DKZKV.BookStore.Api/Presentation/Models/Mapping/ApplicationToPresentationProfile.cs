using AutoMapper;
using DKZKV.BookStore.Application.Queries.QueryModels;

namespace DKZKV.BookStore.Presentation.Models.Mapping;

/// <inheritdoc />
public class ApplicationToPresentationProfile : Profile
{
    /// <inheritdoc />
    public ApplicationToPresentationProfile()
    {
        CreateMap<BookFilter, Application.Queries.Books.BookFilter>()
            .ForMember(dest => dest.Offset, opt => opt.Condition(source => source.Offset != null))
            .ForMember(dest => dest.Count, opt => opt.Condition(source => source.Count != null))
            .IncludeAllDerived();

        CreateMap<IAuthor, Author>();
    }
}