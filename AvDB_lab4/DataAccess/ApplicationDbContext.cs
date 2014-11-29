using System.Data.Entity;
using AvDB_lab4.DataAccess.Mappings;
using AvDB_lab4.Entities.Clients;
using AvDB_lab4.Entities.Credits.Tasks.Approvals;
using AvDB_lab4.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AvDB_lab4.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CustomConnection", throwIfV1Schema: false)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BaseClientMap());
            modelBuilder.Configurations.Add(new LegalPersonMap());
            modelBuilder.Configurations.Add(new JuridicalPersonMap());
            modelBuilder.Configurations.Add(new CurrencyMap());
            modelBuilder.Configurations.Add(new CreditCategoryMap());
            modelBuilder.Configurations.Add(new CreditApplicationMap());
            modelBuilder.Configurations.Add(new BaseTaskMap());
            modelBuilder.Configurations.Add(new ApprovalTaskMap());
            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}