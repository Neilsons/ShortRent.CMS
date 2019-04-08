using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Core.Domain
{
    /// <summary>
    /// 历史操作表
    /// </summary>
    public class HistoryOperator:BaseEntity
    {
        /// <summary>
        /// 操作的模块名称
        /// </summary>
         public string EntityModule { get; set; }
        /// <summary>
        /// 具体的操作
        /// </summary>
         public string Operates { get; set; }
        /// <summary>
        /// 详细描述
        /// </summary>
         public string DetailDescirption { get; set; }
         /// <summary>
         /// 创建时间
         /// </summary>
         public DateTime CreateTime { get; set; }
         /// <summary>
         /// 是否删除 false 不删除 true 删除
         /// </summary>
         [DefaultValue(false)]
         public bool IsDelete { get; set; }
        /// <summary>
        /// person表中的外键
        /// </summary>
         public int PersonId { get; set; }
        /// <summary>
        /// 导航属性 人
        /// </summary>
        public virtual Person Person { get; set; }


    }
}
