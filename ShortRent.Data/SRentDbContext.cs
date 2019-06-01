﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortRent.Core.Domain;
//转换器
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;

namespace ShortRent.Data
{
    /// <summary>
    /// 这是一个上下文类 继承IDbContext
    /// </summary>
    public class SRentDbContext : DbContext,IDbContext
    {
        /// <summary>
        /// 告诉数据库如何初始化
        /// </summary>
        static SRentDbContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SRentDbContext>());
        }
        public SRentDbContext():base("sRentDatabase")
        {

        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(sql, parameters);
        }
        public IEnumerable<T> ExecuteSqlQuery<T>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<T>(sql, parameters).AsEnumerable();
        }
        IDbSet<TEntity> IDbContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }
        #region  override 方法
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //解决表名加s的
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //ef6.0以后  搜索全部实现了entityTypeConfiguration<TEntity>这个的自动加进去
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        #endregion

        public DbSet<Person> Persons { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<EntityPermission> EntityPermissions { get; set; }
        public DbSet<Manager> Managers { get; set; }

        public DbSet<IconsInfo> IconsInfos { get; set; }
    }
}
