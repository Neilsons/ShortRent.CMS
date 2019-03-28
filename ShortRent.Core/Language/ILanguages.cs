using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Language
{
    public interface ILanguages
    {
        Language Default { get; }
        Language[] Supported { get; }
        Language Current { get; set; }

        Language this[String abbreviation] { get; }
    }
}
