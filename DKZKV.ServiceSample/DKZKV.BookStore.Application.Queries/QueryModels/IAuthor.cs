namespace DKZKV.BookStore.Application.Queries.QueryModels;

public interface IAuthor
{
    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public DateTime BirthDate { get; }
    public DateTime? DeathDate { get; }
}