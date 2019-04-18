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
                new Role() { Name = "ϵͳ����Ա", Type = true, CreateTime = DateTime.Now },
                new Role() { Name = "��ͨ�û�", Type = false, CreateTime = DateTime.Now },
                new Role() { Name = "�����û�", Type = true, CreateTime = DateTime.Now },
                new Role() { Name = "ǰ̨��ͨ�û�", Type = false, CreateTime = DateTime.Now },
                new Role() { Name = "ǰ̨��Ƹ�û�", Type = false, CreateTime = DateTime.Now }
                );
            context.Managers.AddOrUpdate(
                new Manager() { ID = 1, Name = "ϵͳ����", ActionName = "", ControllerName = "", ClassIcons = "fa fa-birthday-cake", Color = "#ef5350", Activity = true, CreateTime = DateTime.Now, Pid = null },
                new Manager() { ID = 2, Name = "��־����", ActionName = "List", ControllerName = "LogInfo", ClassIcons = "fa fa-facebook", Color = "#f8bbd0", Activity = true, CreateTime = DateTime.Now, Pid = 1 },
                new Manager() {ID=3, Name = "�˵�����", ActionName = "List", ControllerName = "Manager", ClassIcons = "fa fa-address-book-o", Color = "#ce93d8", Activity = true, CreateTime = DateTime.Now, Pid = 1 },
                new Manager() { ID = 4, Name = "��ɫ����", ActionName = "List", ControllerName = "Role", ClassIcons = "fa fa-apple", Color = "#ea80fc", Activity = true, CreateTime = DateTime.Now, Pid = 1 },
                new Manager() { ID = 5, Name = "�û�����", ActionName = "", ControllerName = "", ClassIcons = "fa fa-adjust", Color = "#c2185b", Activity = true, CreateTime = DateTime.Now, Pid = null },
                new Manager() { ID = 6, Name = "�û�����", ActionName = "List", ControllerName = "Person", ClassIcons = "fa fa-adjust", Color = "#c2185b", Activity = true, CreateTime = DateTime.Now, Pid = 5 }
                );
        }
    }
}
