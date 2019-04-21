using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Web
{
    public interface IAutnenticationProvider
    {
        void SignIn(Person model, bool readmeMe);
        void SignOut();
        Person GetAutnenticationPerson();
    }
}
