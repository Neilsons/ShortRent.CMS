namespace ShortRent.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Core.Domain;

    internal sealed class Configuration : DbMigrationsConfiguration<ShortRent.Data.SRentDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(ShortRent.Data.SRentDbContext context)
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
            context.Persons.AddOrUpdate(
                new Person() { Name = "admin", Birthday = DateTime.Now.AddYears(-18), WeChat = "15141545", Qq = "1584541", CreateTime = DateTime.Now, CreditScore = 100, IdCard = "141145512514785141", PassWord = "123", PerImage = "admin.jpg", Type = true, PerOrder = 1, Sex = true },
                new Person() { Name = "test", Birthday = DateTime.Now.AddYears(-20), WeChat = "1145425445", Qq = "14141414", CreateTime = DateTime.Now, CreditScore = 100, IdCard = "111145512514785141", PassWord = "456", PerImage = "admin.jpg", Type = true, PerOrder = 2, Sex = false }
            );
            context.Roles.AddOrUpdate(
                new Role() { Name = "系统管理员", Type = true, CreateTime = DateTime.Now },
                new Role() { Name = "普通用户", Type = false, CreateTime = DateTime.Now }
                );           
        }
    }
}
