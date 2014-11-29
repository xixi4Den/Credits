using System.Data.Entity.ModelConfiguration;
using AvDB_lab4.Entities.Credits.Tasks;

namespace AvDB_lab4.DataAccess.Mappings
{
    public class BaseTaskMap: EntityTypeConfiguration<BaseTask>
    {
        public BaseTaskMap()
        {
            ToTable("BaseTask");

            HasKey(x => x.Id);

            Property(x => x.Id).IsRequired();
            Property(x => x.UserId).IsRequired();
            Property(x => x.CreditApplicationId).IsRequired();
            Property(x => x.DispalyText).IsRequired();
            Property(x => x.Status).IsRequired();
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.CompleteDate).IsOptional();

            HasRequired(x => x.CreditApplication).WithMany().HasForeignKey(x => x.CreditApplicationId);
            HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}