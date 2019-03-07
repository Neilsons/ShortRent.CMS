using ShortRent.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace ShortRent.WebCore.Infrastructure
{
    /// <summary>
    /// bin目录的类型查找器
    /// </summary>
    public class WebTypeFinder:AppDomainTypeFinder
    {
        #region  Field
        /// <summary>
        /// 表示在bin下面查找到没有，装载了没有
        /// </summary>
        private bool BinFolderAssembilesLoaded = false;
        #endregion
        #region  Method
        /// <summary>
        /// 定义为虚方法可能的是可能为不同的目录
        /// </summary>
        /// <returns></returns>
        public virtual string GetBinDirectory()
        {
            //如果部署到iis上这样查找
            if(HostingEnvironment.IsHosted)
            {
                return HttpRuntime.BinDirectory;
            }
            return AppDomain.CurrentDomain.BaseDirectory;
        }
        public override IList<Assembly> GetAssemblies()
        {
            if(!BinFolderAssembilesLoaded)
            {
                //是否装载
                BinFolderAssembilesLoaded = true;
                LoadMatchingAssemblies(GetBinDirectory());
            }
            return base.GetAssemblies();

        }
        #endregion
    }
}
