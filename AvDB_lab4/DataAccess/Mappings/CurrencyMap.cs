using System.Data.Entity.ModelConfiguration;
using AvDB_lab4.Entities.Currencies;

namespace AvDB_lab4.DataAccess.Mappings
{
    public class CurrencyMap : EntityTypeConfiguration<Currency>
    {
        public CurrencyMap()
        {
            ToTable("Currency");

            HasKey(x => x.Id);

            Property(x => x.FullName).IsRequired();
            Property(x => x.Abbreviation).IsRequired().HasMaxLength(3);
            Property(x => x.Rate).IsRequired();
        }
    }
}