using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.WebCore.Security
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
            permissions.Add(new Permission() { Name = "PersionCreate", Category = "用户创建", Description = "创建" });

            return permissions;
        }
    }
}
