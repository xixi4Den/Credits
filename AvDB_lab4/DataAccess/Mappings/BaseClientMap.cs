using System.Data.Entity.ModelConfiguration;
using AvDB_lab4.Entities.Clients;

namespace AvDB_lab4.DataAccess.Mappings
{
    public class BaseClientMap : EntityTypeConfiguration<BaseClient>
    {
        public BaseClientMap()
        {
            ToTable("BaseClient");

            HasKey(x => x.Id);

            Property(x => x.ClientGroup).IsRequired();
        }
    }
}