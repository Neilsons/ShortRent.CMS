using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data
{
    /// <summary>
    /// 这个接口是为了把ef解耦出来
    /// </summary>
    public interface IDbContext
    {
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        
        IDbSet<TEntity> Set<TEntity>() where TEntity:class;
        /// <summary>
        /// 保存变更
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// 执行sql命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql, params object[] parameters);
        IEnumerable<T> ExecuteSqlQuery<T>(string sql, params object[] parameters);
    }
}
