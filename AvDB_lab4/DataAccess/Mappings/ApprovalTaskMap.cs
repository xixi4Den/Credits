using System.Data.Entity.ModelConfiguration;
using AvDB_lab4.Entities.Credits.Tasks.Approvals;

namespace AvDB_lab4.DataAccess.Mappings
{
    public class ApprovalTaskMap : EntityTypeConfiguration<ApprovalTask>
    {
        public ApprovalTaskMap()
        {
            ToTable("ApprovalTask");

            HasKey(x => x.Id);

            Property(x => x.Id).IsRequired();
            Property(x => x.ApprovalType).IsRequired();
            Property(x => x.Outcome).IsOptional();
            Property(x => x.RejectionReason).IsOptional();
        }
    }
}