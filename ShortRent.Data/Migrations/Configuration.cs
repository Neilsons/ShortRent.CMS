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
                new Person() { Name = "admin", Birthday = DateTime.Now.AddYears(-18), WeChat = "15141545", Qq = "1584541", CreateTime = DateTime.Now, CreditScore = 100, IdCard = "141145512514785141", PassWord = "123", PerImage = "admin.jpg", Type = true, PerOrder = 1, Sex = true,Position="系统管理员",PersonDetail="处理管理后台系统的一切任务" },
                new Person() { Name = "test", Birthday = DateTime.Now.AddYears(-20), WeChat = "1145425445", Qq = "14141414", CreateTime = DateTime.Now, CreditScore = 100, IdCard = "111145512514785141", PassWord = "456", PerImage = "admin.jpg", Type = true, PerOrder = 2, Sex = false,Position="运维测试员",PersonDetail="负责处理客户遇到的所有问题" }
            );
            context.Roles.AddOrUpdate(
                new Role() { Name = "系统管理员", Type = true, CreateTime = DateTime.Now },
                new Role() { Name = "普通用户", Type = false, CreateTime = DateTime.Now },
                new Role() { Name = "测试用户", Type = true, CreateTime = DateTime.Now },
                new Role() { Name = "前台普通用户", Type = false, CreateTime = DateTime.Now },
                new Role() { Name = "前台招聘用户", Type = false, CreateTime = DateTime.Now }
                );
            context.Managers.AddOrUpdate(
                new Manager() {ID=1, Name = "信息管理", ActionName = "", ControllerName = "", ClassIcons = "fa fa-birthday-cake", Color = "#ef5350", Activity = true, CreateTime = DateTime.Now, Pid = null },
                new Manager() { ID = 2, Name = "历史操作管理", ActionName = "List", ControllerName = "HistoryOperator", ClassIcons = "fa fa-address-book-o", Color = "#ce93d8", Activity = true, CreateTime = DateTime.Now, Pid = 1 },
                new Manager() { ID =3, Name = "图标管理", ActionName = "List", ControllerName = "IconsInfo", ClassIcons = "fa fa-apple", Color = "#ea80fc", Activity = true, CreateTime = DateTime.Now, Pid = 15 },
                new Manager() { ID = 4, Name = "介绍信息管理", ActionName = "List", ControllerName = "PerOrComIntroGuidance", ClassIcons = "fa fa-adjust", Color = "#c2185b", Activity = true, CreateTime = DateTime.Now, Pid = 1 },
                new Manager() { ID = 5, Name = "日志管理", ActionName = "List", ControllerName = "LogInfo", ClassIcons = "fa fa-facebook", Color = "#f8bbd0", Activity = true, CreateTime = DateTime.Now, Pid = 1 },
                new Manager() { ID = 6, Name = "标签管理", ActionName = "List", ControllerName = "CompanyPerTags", ClassIcons = "fa fa-facebook", Color = "#f8bbd0", Activity = true, CreateTime = DateTime.Now, Pid = 1 },
                new Manager() {ID=7, Name = "菜单管理", ActionName = "List", ControllerName = "Manager", ClassIcons = "fa fa-address-book-o", Color = "#ce93d8", Activity = true, CreateTime = DateTime.Now, Pid = 15 },
                new Manager() { ID = 8, Name = "角色管理", ActionName = "List", ControllerName = "Role", ClassIcons = "fa fa-apple", Color = "#ea80fc", Activity = true, CreateTime = DateTime.Now, Pid = 1 },
                new Manager() { ID = 9, Name = "后台用户管理", ActionName = "", ControllerName = "", ClassIcons = "fa fa-adjust", Color = "#c2185b", Activity = true, CreateTime = DateTime.Now, Pid = null },
                new Manager() { ID = 10, Name = "后台用户", ActionName = "List", ControllerName = "Person", ClassIcons = "fa fa-adjust", Color = "#c2185b", Activity = true, CreateTime = DateTime.Now, Pid = 9 },
                new Manager() { ID=11,Name="前台用户管理", ActionName = "", ControllerName = "", ClassIcons = "fa fa-adjust", Color = "#c2185b", Activity = true, CreateTime = DateTime.Now, Pid = null },
                new Manager() { ID = 12, Name = "公司管理", ActionName = "List", ControllerName = "Company", ClassIcons = "fa fa-adjust", Color = "#c2185b", Activity = true, CreateTime = DateTime.Now, Pid = 11 },
                new Manager() { ID = 13, Name = "被招聘者管理", ActionName = "List", ControllerName = "UserType", ClassIcons = "fa fa-adjust", Color = "#c2185b", Activity = true, CreateTime = DateTime.Now, Pid = 11 },
                new Manager() { ID=14,Name="招聘者管理",ActionName= "ReduitList", ControllerName="UserType",ClassIcons= "fa fa-adjust", Color = "#c2185b", Activity = true, CreateTime = DateTime.Now, Pid = 11 },
                new Manager() { ID = 15, Name = "系统管理", ActionName = "", ControllerName = "", ClassIcons = "fa fa-adjust", Color = "#c2185b", Activity = true, CreateTime = DateTime.Now, Pid =null },
                new Manager() { ID = 16, Name = "联系信息管理", ActionName = "List", ControllerName = "Contact", ClassIcons = "fa fa-adjust", Color = "#c2185b", Activity = true, CreateTime = DateTime.Now, Pid = 1 },
                new Manager() { ID = 17, Name = "行业管理", ActionName = "List", ControllerName = "Bussiness", ClassIcons = "fa fa-adjust", Color = "#c2185b", Activity = true, CreateTime = DateTime.Now, Pid = 1 },
                new Manager() { ID = 18, Name = "发布信息管理", ActionName = "", ControllerName = "", ClassIcons = "fa fa-adjust", Color = "#c2185b", Activity = true, CreateTime = DateTime.Now, Pid = null },
                new Manager() { ID = 19, Name = "被招聘者管理", ActionName = "RecruiterByList", ControllerName = "PublishMsg", ClassIcons = "fa fa-adjust", Color = "#c2185b", Activity = true, CreateTime = DateTime.Now, Pid = 18 },
                 new Manager() { ID = 20, Name = "招聘者管理", ActionName = "RecruiterList", ControllerName = "PublishMsg", ClassIcons = "fa fa-adjust", Color = "#c2185b", Activity = true, CreateTime = DateTime.Now, Pid = 18 }
                );
            context.IconsInfos.AddOrUpdate(
                new IconsInfo { ID=1,prefix="fa",Content= "fa-birthday-cake" },
                new IconsInfo { ID = 2, prefix = "fa", Content = "fa-facebook" },
                new IconsInfo { ID = 3, prefix = "fa", Content = "fa-address-book-o" },
                new IconsInfo { ID = 4, prefix = "fa", Content = "fa-apple" },
                new IconsInfo { ID = 5, prefix = "fa", Content = "fa-adjust" }
                );
        }

       
    }
}
