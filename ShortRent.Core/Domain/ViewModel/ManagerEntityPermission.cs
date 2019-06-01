using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    public class ManagerEntityPermission
    {
        /// <summary>
        /// 实体ID
        /// </summary>
        public int EntityId { get; set; }

        public int roleId { get; set; }

        public string ManagerName { get; set; }
    }
}
