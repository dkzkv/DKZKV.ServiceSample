using AutoMapper;
using DKZKV.BookStore.Application.Queries.QueryModels;
using DKZKV.BookStore.Domain.AggregatesModel.Author;
using DKZKV.BookStore.Domain.SeedWork;
using DKZKV.BookStore.Persistence.Mapping.AuxiliaryModels;
using JetBrains.Annotations;
using Author = DKZKV.BookStore.Persistence.Entities.Author;
using Book = DKZKV.BookStore.Domain.AggregatesModel.Author.Book;

namespace DKZKV.BookStore.Persistence.Mapping;

[UsedImplicitly]
public class PersistenceToDomainProfile : Profile
{
    public PersistenceToDomainProfile()
    {
        CreateMap<Author, Domain.AggregatesModel.Author.Author>()
            .ConstructUsing((o, m) =>
                new DomainAuthorAuxiliaryModel(o.Id,
                    o.FirstName,
                    o.LastName,
                    new DateOnly(o.BirthDate.Year, o.BirthDate.Month, o.BirthDate.Day),
                    o.DeathDate.HasValue ? new DateOnly(o.DeathDate.Value.Year, o.DeathDate.Value.Month, o.DeathDate.Value.Day) : null,
                    m.Mapper.Map<IEnumerable<Book>>(o.Books)
                ));

        CreateMap<Entities.Book, Book>().ConstructUsing((o, m) =>
            new DomainBookAuxiliaryModel(o.Id,
                o.AuthorId,
                o.Name,
                new DateOnly(o.WroteAt.Year, o.WroteAt.Month, o.WroteAt.Day),
                Enumeration.FromValue<BookStyle>(o.Style)
            ));


        CreateMap<Author, IAuthor>().As<AuthorAuxiliaryModel>();
        CreateMap<Author, AuthorAuxiliaryModel>();

        CreateMap<Entities.Book, IBook>().As<BookAuxiliaryModel>();
        CreateMap<Entities.Book, BookAuxiliaryModel>()
            .ForMember(dest => dest.BookName, o => o.MapFrom(source => source.Name))
            .ForMember(dest => dest.Style, o => o.MapFrom(source => Enumeration.FromValue<BookStyle>(source.Style)));

        CreateMap<Entities.Book, IBookShortInfo>().As<BookShortInfoAuxiliaryModel>();
        CreateMap<Entities.Book, BookShortInfoAuxiliaryModel>()
            .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
            .ForMember(dest => dest.BookName, o => o.MapFrom(source => source.Name))
            .ForMember(dest => dest.AuthorId, o => o.MapFrom(source => source.AuthorId))
            .ForMember(dest => dest.AuthorFirstName, o => o.MapFrom(source => source.Author.FirstName))
            .ForMember(dest => dest.AuthorLastName, o => o.MapFrom(source => source.Author.LastName));
    }
}