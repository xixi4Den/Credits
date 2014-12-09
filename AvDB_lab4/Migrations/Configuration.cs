using System;
using System.Linq;
using AvDB_lab4.DataAccess;
using System.Data.Entity.Migrations;
using AvDB_lab4.Entities.Clients;
using AvDB_lab4.Entities.Credits;
using AvDB_lab4.Entities.Currencies;
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
            //var category = new CreditCategory
            //{
            //    Id = Guid.NewGuid(),
            //    ClientGroup = ClientGroup.PrivatePerson,
            //    CurrencyId = new Guid("585047B7-4E02-4190-90BD-4955111C70BA"),
            //    DisplayText = "Test",
            //    IsEarlyRepayment = false,
            //    MaxAmount = 1000000,
            //    Name = "TestCategory1",
            //    Rate = 0.41,
            //    RepaymentScheme = RepaymentScheme.Annuity,
            //};
            //context.Set<CreditCategory>().Add(category);
            //context.SaveChanges();

            //var currency = new Currency
            //{
            //    Id = Guid.NewGuid(),
            //    Abbreviation = "TST",
            //    FullName = "Test Currency",
            //    Rate = 1000000,
            //};
            //context.Set<Currency>().Add(currency);
            //context.SaveChanges();

            //const string defaultRole = "CreditCommitteeEmployee";
            //const string defaultUser = "TestUser4";
            //const string defaultPassword = "123456qW!";

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
            //    var user = context.Users.Single(u => u.UserName.Equals(defaultUser, StringComparison.CurrentCultureIgnoreCase));
            //    var store = new UserStore<ApplicationUser>(context);
            //    var manager = new UserManager<ApplicationUser>(store);
            //    manager.AddToRole(user.Id, defaultRole);
            //}
        }
    }
}
