using System.Data.Entity.ModelConfiguration;
using AvDB_lab4.Entities.Credits;

namespace AvDB_lab4.DataAccess.Mappings
{
    public class CreditApplicationMap: EntityTypeConfiguration<CreditApplication>
    {
        public CreditApplicationMap()
        {
            ToTable("CreditApplication");

            HasKey(x => x.Id);

            Property(x => x.RegisterDate).IsRequired();
            Property(x => x.CompleteDate).IsOptional();
            Property(x => x.ClientId).IsRequired();
            Property(x => x.CreditCategoryId).IsRequired();
            Property(x => x.IsCompleted).IsRequired();
            Property(x => x.Outcome).IsOptional();
            Property(x => x.RejectionReason).IsOptional();

            HasRequired(x => x.Client).WithMany().HasForeignKey(x => x.ClientId);
            HasRequired(x => x.CreditCategory).WithMany().HasForeignKey(x => x.CreditCategoryId).WillCascadeOnDelete(false);

            HasMany(t => t.Attachments);
        }
    }
}