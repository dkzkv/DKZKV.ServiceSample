using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#pragma warning disable CS8618
namespace DKZKV.BookStore.Persistence.Entities;

public class Book : IEntityTypeConfiguration<Book>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime WroteAt { get; set; }
    public Guid AuthorId { get; set; }
    public Author Author { get; set; }
    public short Style { get; set; }
    public DateTime? DeletedAtUtc { get; set; }

    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Book")
            .HasKey(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedNever();

        builder.Property(o => o.Name)
            .IsRequired()
            .HasColumnType("varchar(128)");

        builder.Property(o => o.WroteAt)
            .IsRequired();

        builder.HasOne(e => e.Author)
            .WithMany(c => c.Books);
    }
}