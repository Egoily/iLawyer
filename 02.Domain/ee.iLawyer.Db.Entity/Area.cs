using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Db.Entity
{
    /// <summary>
    /// 行政区划信息
    /// </summary>
    public class Area
    {
        /// <summary>
        /// 行政区划代码
        /// </summary>
        public virtual string AreaCode { get; set; }
        /// <summary>
        ///  行政区划名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 父行政区划
        /// </summary>
        public virtual Area Parent { get; set; }
        /// <summary>
        /// 辖下行政区划
        /// </summary>
        public virtual IList<Area> Children { get; set; }
    }
}
