using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Persistence.ConfigMaps
{
    public class TransactionConfigMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            // Table name
            builder.ToTable("Transactions");

            // Properties
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Value).HasColumnType("decimal(5,2)").IsRequired(true);
            builder.Property(x => x.Product).IsRequired(true);
            builder.Property(x => x.CreditCardBrand).IsRequired(true);
            builder.Property(x => x.Status).IsRequired(true);
            builder.Property(x => x.NumberOfInstallments).IsRequired(true);
            builder.Property(x => x.CreationDate).IsRequired(true);
            builder.Property(x => x.CustomerId).IsRequired(true);
        }
    }
}