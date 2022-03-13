using AutoMapper;

namespace DKZKV.BookStore.Persistence.Repositories;

public abstract class BaseRepository
{
    protected BaseRepository(IMapper mapper, BookStoreBdContext dbContext)
    {
        Mapper = mapper;
        DbContext = dbContext;
    }

    protected IMapper Mapper { get; }
    protected BookStoreBdContext DbContext { get; }
}