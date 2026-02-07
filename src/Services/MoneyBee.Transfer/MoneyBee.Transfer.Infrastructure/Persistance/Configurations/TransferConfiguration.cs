using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyBee.Transfer.Domain.Entities;

namespace MoneyBee.Transfer.Infrastructure.Persistence.Configurations;

public class TransferConfiguration : IEntityTypeConfiguration<Domain.Entities.Transfer>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Transfer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TransactionCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(x => x.TransactionCode)
            .IsUnique();

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2);

        builder.Property(x => x.Fee)
            .HasPrecision(18, 2);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}