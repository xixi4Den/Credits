using System.Data.Entity.ModelConfiguration;
using AvDB_lab4.Entities.Clients;

namespace AvDB_lab4.DataAccess.Mappings
{
    public class LegalPersonMap : EntityTypeConfiguration<LegalPerson>
    {
        public LegalPersonMap()
        {
            ToTable("LegalPerson");

            HasKey(x => x.Id);

            Property(x => x.FirstName).IsRequired();
            Property(x => x.LastName).IsRequired();
            Property(x => x.PassportDetails).IsRequired();
            Property(x => x.RegistrationAddress).IsRequired();
            Property(x => x.CurrentAddress).IsRequired();
            Property(x => x.HomePhone).IsOptional();
            Property(x => x.MobilePhone).IsRequired();
            Property(x => x.Skype).IsOptional();
            Property(x => x.Email).IsRequired();
            Property(x => x.CompanyName).IsRequired();
            Property(x => x.WorkingPosition).IsRequired();
        }
    }
}