namespace BillsPaymentSystem.Data.EntityConfigurations
{
    using BillsPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder
                .Property(ba => ba.BankName)
                .HasMaxLength(50)
                .IsUnicode(true);

            builder
                 .Property(ba => ba.SWIFT)
                 .HasMaxLength(20)
                 .IsUnicode(false);
        }
    }
}