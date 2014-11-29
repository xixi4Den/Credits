using System.Data.Entity.ModelConfiguration;
using AvDB_lab4.Entities.Credits;

namespace AvDB_lab4.DataAccess.Mappings
{
    public class CreditCategoryMap : EntityTypeConfiguration<CreditCategory>
    {
        public CreditCategoryMap()
        {
            ToTable("CreditCategory");

            HasKey(x => x.Id);

            Property(x => x.Name).IsRequired().HasMaxLength(5);
            Property(x => x.DisplayText).IsRequired();
            Property(x => x.Span).IsRequired();
            Property(x => x.Rate).IsRequired();
            Property(x => x.RepaymentScheme).IsRequired();
            Property(x => x.IsEarlyRepayment).IsRequired();
            Property(x => x.MaxAmount).IsRequired();
            Property(x => x.ClientGroup).IsRequired();
            Property(x => x.CurrencyId).IsRequired();

            HasRequired(x => x.Currency).WithMany().HasForeignKey(x => x.CurrencyId);
        }
    }
}