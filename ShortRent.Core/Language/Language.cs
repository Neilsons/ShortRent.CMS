using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Language
{
    public class Language
    {
        public String Name { get; set; }
        public String Abbreviation { get; set; }

        public Boolean IsDefault { get; set; }
        public CultureInfo Culture { get; set; }
    }
}
