using AutoMapper;
using DKZKV.BookStore.Domain.AggregatesModel.Author;
using JetBrains.Annotations;

namespace DKZKV.BookStore.Persistence.Mapping;

[UsedImplicitly]
public class DomainToPersistenceProfile : Profile
{
    public DomainToPersistenceProfile()
    {
        CreateMap<Author, Entities.Author>()
            .ForMember(dest => dest.BirthDate, o => o.MapFrom(source => source.BirthDate.ToDateTime(new TimeOnly())))
            .ForMember(dest => dest.DeathDate,
                o => o.MapFrom(source => source.DeathDate.HasValue ? source.DeathDate.Value.ToDateTime(new TimeOnly()) : (DateTime?)null))
            .ForMember(destination => destination.DeletedAtUtc, o => o.Ignore());

        CreateMap<Book, Entities.Book>()
            .ForMember(dest => dest.WroteAt, o => o.MapFrom(source => source.WroteAt.ToDateTime(new TimeOnly())))
            .ForMember(dest => dest.Style, o => o.MapFrom(source => source.Style.Id))
            .ForMember(destination => destination.DeletedAtUtc, o => o.Ignore());
    }
}