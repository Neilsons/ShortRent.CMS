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
        IQueryable<Role> GetRoles();
        IQueryable<Permission> GetPermissions(int id);
    }
}
