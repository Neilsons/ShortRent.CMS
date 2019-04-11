using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace ShortRent.Core.Config
{
    public class CacheTimesElement:ConfigurationElement
    {
        #region Fields
        private const string EnablePropertyName = "enable";
        private const string Lev1ChildName = "lev1";
        private const string Lev2ChildName = "lev2";
        #endregion
        #region Perperty
        /// <summary>
        /// 是否允许配置解析
        /// </summary>
        [ConfigurationProperty(EnablePropertyName,IsRequired =true)]
        public bool Enable
        {
            get { return (bool)base[EnablePropertyName]; }
            set { base[EnablePropertyName] = value; }
        }
        [ConfigurationProperty(Lev1ChildName, IsRequired = true)]
        public LevElement Level1
        {
            get { return base[Lev1ChildName] as LevElement; }
            set { base[Lev1ChildName] = value; }
        }
        [ConfigurationProperty(Lev2ChildName, IsRequired = true)]
        public LevElement Level2
        {
            get { return base[Lev2ChildName] as LevElement; }
            set { base[Lev2ChildName] = value; }
        }
        #endregion



    }
}
