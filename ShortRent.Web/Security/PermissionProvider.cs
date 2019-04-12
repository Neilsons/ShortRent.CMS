using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
            permissions.Add(new Permission() { Name = "PersonCreate", Category = "用户管理", Type=true, Description = "创建",ID=1 });
            permissions.Add(new Permission() { Name = "PersonIndex", Category = "用户管理",Type=true,Description = "列表",ID=2 });
            permissions.Add(new Permission() { Name = "RoleList", Category = "角色管理", Type = true, Description = "列表", ID = 3 });
            permissions.Add(new Permission() { Name = "RoleCreate", Category = "角色管理", Type = true, Description = "创建", ID = 4});
            permissions.Add(new Permission() { Name = "RoleEdit", Category = "角色管理", Type = true, Description = "编辑", ID = 5 });
            permissions.Add(new Permission() { Name = "RolePermission", Category = "角色管理", Type = true, Description = "角色分配权限", ID = 6 });
            return permissions;
        }
    }
}
