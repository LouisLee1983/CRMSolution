namespace CrmWebApp.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CrmWebApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CrmWebApp.Models.ApplicationDbContext";
        }

        protected override void Seed(CrmWebApp.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            this.AddUserAndRoles();
        }
        bool AddUserAndRoles()
        {
            bool success = false;


            var idManager = new IdentityManager();
            success = idManager.CreateRole("Admin");
            if (!success == true) return success;


            success = idManager.CreateRole("OtaSales");
            if (!success == true) return success;


            success = idManager.CreateRole("Guest");
            if (!success) return success;



            var newUser = new ApplicationUser()
            {
                UserName = "admin",
                TrueName = "admin",
                QQ = "425451886",
                PhoneNumber = "18180055588",
                Email = "425451886@qq.com"
            };


            success = idManager.CreateUser(newUser, "abcd-1234");
            if (!success) return success;


            success = idManager.AddUserToRole(newUser.Id, "Admin");
            if (!success) return success;


            success = idManager.AddUserToRole(newUser.Id, "OtaSales");
            if (!success) return success;


            success = idManager.AddUserToRole(newUser.Id, "Guest");
            if (!success) return success;


            return success;
        }
    }
}
