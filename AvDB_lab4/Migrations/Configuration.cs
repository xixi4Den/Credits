using System;
using System.Linq;
using AvDB_lab4.DataAccess;
using System.Data.Entity.Migrations;
using AvDB_lab4.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AvDB_lab4.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //const string defaultRole = "Operator";
            //const string defaultUser = "TestUser1";
            //const string defaultPassword = "123456";

            //if (!context.Roles.Any(r => r.Name == defaultRole))
            //{
            //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            //    roleManager.Create(new IdentityRole(defaultRole));
            //}

            //if (!context.Users.Any(u => u.UserName == defaultUser))
            //{
            //    var store = new UserStore<ApplicationUser>(context);
            //    var manager = new UserManager<ApplicationUser>(store);
            //    var user = new ApplicationUser { UserName = defaultUser };
            //    manager.Create(user, defaultPassword);

            //    manager.AddToRole(user.Id, defaultRole);
            //}
            //else
            //{
                //var user = context.Users.Single(u => u.UserName.Equals(defaultUser, StringComparison.CurrentCultureIgnoreCase));
                //var store = new UserStore<ApplicationUser>(context);
                //var manager = new UserManager<ApplicationUser>(store);
                //manager.AddToRole(user.Id, defaultRole);
            //}
        }
    }
}
