using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Data
{
    public interface IRepository<T> where T:class
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Insert(T entity);
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Update(T entity);
        /// <summary>
        /// 删除一套记录
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Delete(T entity);
     
        IEnumerable<T> Entitys { get; }

        IEnumerable<T> IncludeEntitys(string path);
    }
}
