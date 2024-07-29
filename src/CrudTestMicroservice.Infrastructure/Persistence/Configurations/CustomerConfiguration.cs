using CrudTestMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CrudTestMicroservice.Infrastructure.Persistence.Configurations;
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasQueryFilter(p => !p.IsDeleted);
        builder.Ignore(entity => entity.DomainEvents);
        builder.HasKey(entity => entity.Id);
        builder.Property(entity => entity.Id).ValueGeneratedNever();
        builder.Property(entity => entity.FirstName).IsRequired(true);
        builder.Property(entity => entity.LastName).IsRequired(true);
        builder.Property(entity => entity.DateOfBirth).IsRequired(true);
        builder.Property(entity => entity.PhoneNumber).IsRequired(true);
        builder.Property(entity => entity.Email).IsRequired(true);
        builder.HasIndex(p => new {p.FirstName , p.LastName,p.DateOfBirth}).IsUnique(true);
        builder.HasIndex(entity =>new {entity.Email ,entity.IsDeleted}).IsUnique(true);
        builder.Property(entity => entity.BankAccountNumber).IsRequired(true);

    }
}
