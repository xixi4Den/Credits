using System.Data.Entity.ModelConfiguration;
using AvDB_lab4.Entities;

namespace AvDB_lab4.DataAccess.Mappings
{
    public class AttachmentMap: EntityTypeConfiguration<Attachment>
    {
        public AttachmentMap()
        {
            ToTable("Attachment");

            HasKey(x => x.Id);

            Property(x => x.Id).IsRequired();
            Property(x => x.Source).IsRequired();
            Property(x => x.IsContract).IsRequired();
            Property(x => x.ApplicationId).IsRequired();

            HasRequired(t => t.Application)
                .WithMany(t => t.Attachments)
                .HasForeignKey(d => d.ApplicationId);
        }
    }
}