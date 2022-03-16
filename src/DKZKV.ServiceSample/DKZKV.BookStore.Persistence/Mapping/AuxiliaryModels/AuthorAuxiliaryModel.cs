using DKZKV.BookStore.Application.Queries.QueryModels;

#pragma warning disable CS8618
namespace DKZKV.BookStore.Persistence.Mapping.AuxiliaryModels;

internal class AuthorAuxiliaryModel : IAuthor
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
}