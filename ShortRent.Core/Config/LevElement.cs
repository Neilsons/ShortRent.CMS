using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace ShortRent.Core.Config
{
    public class LevElement:ConfigurationElement
    {
        #region Fields
        private const string TimePropertyName = "time";
        #endregion
        #region Property
        [ConfigurationProperty(TimePropertyName,IsRequired =true)]
        public int timeMinutes
        {
            get { return (int)base[TimePropertyName]; }
            set { base[TimePropertyName] = value; }
        }
        #endregion
    }
}
