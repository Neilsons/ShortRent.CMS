using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Web.Security
{
    /// <summary>
    /// 权限提供者  开发者来维护这张表
    /// </summary>
    public class PermissionProvider:IPermissionProvider
    {
        public IEnumerable<Permission> GetPermissions()
        {
            List<Permission> permissions = new List<Permission>();
            //添加权限
            permissions.Add(new Permission() { Name = "PersonCreate", Category = "用户创建", Description = "创建",ID=1 });
            permissions.Add(new Permission() { Name = "PersonIndex", Category = "用户主页", Description = "列表",ID=2 });

            return permissions;
        }
    }
}
