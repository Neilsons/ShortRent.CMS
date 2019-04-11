using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortRent.Core.Data;

namespace ShortRent.Data
{
    //把仓储和ef的dbContext解耦
    public class EfRepository<T> : IRepository<T> where T : class
    {
        #region Field
        //已经把ef解耦
        private readonly IDbContext _dbContext;
        //为了方便缓存一下
        private IDbSet<T> _dbSet;
        #endregion

        #region  Property
        protected virtual IDbSet<T> DbSet
        {
            get
            {
                this._dbSet = this._dbSet ?? _dbContext.Set<T>();
                return this._dbSet;
            }
        }

        public IEnumerable<T> Entitys
        {
            get { return this.DbSet; }
        }
        #endregion

        #region  Constructor 构造
        //将dbContext注入进来
        public EfRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            this.DbSet.Remove(entity);
            this._dbContext.SaveChanges();
        }
        public void Insert(T entity)
        {
            if(entity==null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            this.DbSet.Add(entity);
            this._dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
           if(entity==null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            this._dbContext.SaveChanges();
        }

        #endregion

    }
}
