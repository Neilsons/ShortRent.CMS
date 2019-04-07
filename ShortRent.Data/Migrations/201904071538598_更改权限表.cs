namespace ShortRent.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 更改权限表 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Birthday = c.DateTime(nullable: false),
                        Sex = c.Boolean(),
                        Type = c.Boolean(nullable: false),
                        PerImage = c.String(maxLength: 200),
                        PassWord = c.String(maxLength: 120),
                        CreditScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdCard = c.String(nullable: false, maxLength: 18),
                        PerOrder = c.Int(),
                        Qq = c.String(maxLength: 50),
                        WeChat = c.String(maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Type = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Category = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.Boolean(nullable: false),
                        IdCardFront = c.String(maxLength: 200),
                        IdCardBack = c.String(maxLength: 200),
                        CompanyName = c.String(maxLength: 100),
                        CompanyImg = c.String(maxLength: 500),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        PerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.PerId, cascadeDelete: true)
                .Index(t => t.PerId);
            
            CreateTable(
                "dbo.PerOrComIntro",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Answer = c.String(nullable: false, maxLength: 500),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        UserTypeInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PerOrComIntroGuidance", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.UserType", t => t.UserTypeInfoId, cascadeDelete: true)
                .ForeignKey("dbo.UserType", t => t.UserTypeInfoId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.UserTypeInfoId)
                .Index(t => t.UserTypeInfoId);
            
            CreateTable(
                "dbo.PerOrComIntroGuidance",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuestionMsg = c.String(nullable: false, maxLength: 200),
                        Type = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PublishMsg",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Phone = c.String(maxLength: 20),
                        Address = c.String(maxLength: 200),
                        Email = c.String(maxLength: 50),
                        Decription = c.String(maxLength: 200),
                        Detail = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        UserTypeInfoId = c.Int(nullable: false),
                        BusinessTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Bussiness", t => t.BusinessTypeId, cascadeDelete: true)
                .ForeignKey("dbo.UserType", t => t.UserTypeInfoId)
                .ForeignKey("dbo.UserType", t => t.UserTypeInfoId)
                .Index(t => t.BusinessTypeId)
                .Index(t => t.UserTypeInfoId)
                .Index(t => t.UserTypeInfoId);
            
            CreateTable(
                "dbo.Bussiness",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        NameSpell = c.String(nullable: false, maxLength: 200),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Discuss",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Message = c.String(nullable: false, maxLength: 200),
                        Floor = c.Int(),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        PublishId = c.Int(nullable: false),
                        UserTypeInfoId = c.Int(nullable: false),
                        ParentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Discuss", t => t.ParentId)
                .ForeignKey("dbo.UserType", t => t.UserTypeInfoId, cascadeDelete: true)
                .ForeignKey("dbo.PublishMsg", t => t.PublishId, cascadeDelete: true)
                .Index(t => t.ParentId)
                .Index(t => t.UserTypeInfoId)
                .Index(t => t.PublishId);
            
            CreateTable(
                "dbo.AddressInfo",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(maxLength: 100),
                        PerId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AddressInfo", t => t.PerId)
                .Index(t => t.PerId);
            
            CreateTable(
                "dbo.Collect",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserTypeInfoId = c.Int(nullable: false),
                        CollectCompanyId = c.Int(nullable: false),
                        CollectUserId = c.Int(nullable: false),
                        Type = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserType", t => t.CollectUserId, cascadeDelete: true)
                .ForeignKey("dbo.Company", t => t.CollectCompanyId, cascadeDelete: true)
                .ForeignKey("dbo.UserType", t => t.UserTypeInfoId)
                .Index(t => t.CollectUserId)
                .Index(t => t.CollectCompanyId)
                .Index(t => t.UserTypeInfoId);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Score = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Introduction = c.String(),
                        CompanyImg = c.String(maxLength: 500),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CompanyPerTag",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Color = c.String(nullable: false, maxLength: 10),
                        Type = c.Boolean(nullable: false),
                        TagOrder = c.Int(nullable: false),
                        IsCompany = c.Boolean(nullable: false),
                        PerOrComId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EntityPermission",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        RoleInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.RoleInfoId, cascadeDelete: true)
                .Index(t => t.RoleInfoId);
            
            CreateTable(
                "dbo.HistoryRecord",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        UserTypeInfoId = c.Int(nullable: false),
                        PublishId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PublishMsg", t => t.PublishId, cascadeDelete: true)
                .ForeignKey("dbo.UserType", t => t.UserTypeInfoId, cascadeDelete: true)
                .Index(t => t.PublishId)
                .Index(t => t.UserTypeInfoId);
            
            CreateTable(
                "dbo.Manager",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        ControllerName = c.String(maxLength: 50),
                        ActionName = c.String(maxLength: 50),
                        Activity = c.Boolean(nullable: false),
                        ClassIcons = c.String(maxLength: 50),
                        Color = c.String(maxLength: 50),
                        Pid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Manager", t => t.Pid)
                .Index(t => t.Pid);
            
            CreateTable(
                "dbo.RolePermission",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.PermissionId })
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Permission", t => t.PermissionId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonId, t.RoleId })
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Manager", "Pid", "dbo.Manager");
            DropForeignKey("dbo.HistoryRecord", "UserTypeInfoId", "dbo.UserType");
            DropForeignKey("dbo.HistoryRecord", "PublishId", "dbo.PublishMsg");
            DropForeignKey("dbo.EntityPermission", "RoleInfoId", "dbo.Role");
            DropForeignKey("dbo.Collect", "UserTypeInfoId", "dbo.UserType");
            DropForeignKey("dbo.Collect", "CollectCompanyId", "dbo.Company");
            DropForeignKey("dbo.Collect", "CollectUserId", "dbo.UserType");
            DropForeignKey("dbo.AddressInfo", "PerId", "dbo.AddressInfo");
            DropForeignKey("dbo.PublishMsg", "UserTypeInfoId", "dbo.UserType");
            DropForeignKey("dbo.PublishMsg", "UserTypeInfoId", "dbo.UserType");
            DropForeignKey("dbo.Discuss", "PublishId", "dbo.PublishMsg");
            DropForeignKey("dbo.Discuss", "UserTypeInfoId", "dbo.UserType");
            DropForeignKey("dbo.Discuss", "ParentId", "dbo.Discuss");
            DropForeignKey("dbo.PublishMsg", "BusinessTypeId", "dbo.Bussiness");
            DropForeignKey("dbo.UserType", "PerId", "dbo.Person");
            DropForeignKey("dbo.PerOrComIntro", "UserTypeInfoId", "dbo.UserType");
            DropForeignKey("dbo.PerOrComIntro", "UserTypeInfoId", "dbo.UserType");
            DropForeignKey("dbo.PerOrComIntro", "QuestionId", "dbo.PerOrComIntroGuidance");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.UserRole", "PersonId", "dbo.Person");
            DropForeignKey("dbo.RolePermission", "PermissionId", "dbo.Permission");
            DropForeignKey("dbo.RolePermission", "RoleId", "dbo.Role");
            DropIndex("dbo.Manager", new[] { "Pid" });
            DropIndex("dbo.HistoryRecord", new[] { "UserTypeInfoId" });
            DropIndex("dbo.HistoryRecord", new[] { "PublishId" });
            DropIndex("dbo.EntityPermission", new[] { "RoleInfoId" });
            DropIndex("dbo.Collect", new[] { "UserTypeInfoId" });
            DropIndex("dbo.Collect", new[] { "CollectCompanyId" });
            DropIndex("dbo.Collect", new[] { "CollectUserId" });
            DropIndex("dbo.AddressInfo", new[] { "PerId" });
            DropIndex("dbo.PublishMsg", new[] { "UserTypeInfoId" });
            DropIndex("dbo.PublishMsg", new[] { "UserTypeInfoId" });
            DropIndex("dbo.Discuss", new[] { "PublishId" });
            DropIndex("dbo.Discuss", new[] { "UserTypeInfoId" });
            DropIndex("dbo.Discuss", new[] { "ParentId" });
            DropIndex("dbo.PublishMsg", new[] { "BusinessTypeId" });
            DropIndex("dbo.UserType", new[] { "PerId" });
            DropIndex("dbo.PerOrComIntro", new[] { "UserTypeInfoId" });
            DropIndex("dbo.PerOrComIntro", new[] { "UserTypeInfoId" });
            DropIndex("dbo.PerOrComIntro", new[] { "QuestionId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "PersonId" });
            DropIndex("dbo.RolePermission", new[] { "PermissionId" });
            DropIndex("dbo.RolePermission", new[] { "RoleId" });
            DropTable("dbo.UserRole");
            DropTable("dbo.RolePermission");
            DropTable("dbo.Manager");
            DropTable("dbo.HistoryRecord");
            DropTable("dbo.EntityPermission");
            DropTable("dbo.CompanyPerTag");
            DropTable("dbo.Company");
            DropTable("dbo.Collect");
            DropTable("dbo.AddressInfo");
            DropTable("dbo.Discuss");
            DropTable("dbo.Bussiness");
            DropTable("dbo.PublishMsg");
            DropTable("dbo.PerOrComIntroGuidance");
            DropTable("dbo.PerOrComIntro");
            DropTable("dbo.UserType");
            DropTable("dbo.Permission");
            DropTable("dbo.Role");
            DropTable("dbo.Person");
        }
    }
}
