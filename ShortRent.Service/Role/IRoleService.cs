using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public interface IRoleService
    {
        List<Role> GetRoles();
        List<Role> GetRoles(int pageSize, int pageNumber, out int total);
        Role GetRole(int id, bool? type=null);
        List<Permission> GetPermissions(int id);
        List<Role> GetRoles(int pageSize, int pageNumber, string roleName, out int total);
        void CreateRole(Role role);
        void UpdateRole(Role role);
    }
}
