using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#pragma warning disable CS8618
namespace DKZKV.BookStore.Persistence.Entities;

public class Author : IEntityTypeConfiguration<Author>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }

    public ICollection<Book> Books { get; set; }
    public DateTime? DeletedAtUtc { get; set; }

    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Author")
            .HasKey(o => o.Id);

        builder.Property(o => o.Id).ValueGeneratedNever();

        builder.Property(o => o.FirstName)
            .IsRequired()
            .HasColumnType("varchar(128)");
        builder.Property(o => o.LastName)
            .IsRequired()
            .HasColumnType("varchar(128)");
        builder.Property(o => o.BirthDate)
            .IsRequired();

        builder.HasMany(x => x.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId);
    }
}