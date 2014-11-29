using System.Data.Entity.ModelConfiguration;
using AvDB_lab4.Entities;
using AvDB_lab4.Entities.Clients;

namespace AvDB_lab4.DataAccess.Mappings
{
    public class JuridicalPersonMap : EntityTypeConfiguration<JuridicalPerson>
    {
        public JuridicalPersonMap()
        {
            ToTable("JuridicalPerson");

            HasKey(x => x.Id);

            Property(x => x.Name).IsRequired();
            Property(x => x.LegalAddress).IsRequired();
            Property(x => x.Director).IsRequired();
            Property(x => x.AccountantGeneral).IsRequired();
        }
    }
}