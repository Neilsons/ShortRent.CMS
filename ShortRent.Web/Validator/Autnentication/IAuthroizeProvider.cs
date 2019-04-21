using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Web
{
    public interface IAuthroizeProvider
    {
        bool Authorize(string permissionName, Person person);
        bool Authorize(string permissionName);
    }
}
