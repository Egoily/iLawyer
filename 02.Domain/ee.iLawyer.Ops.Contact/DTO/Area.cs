using System;
using System.Collections.Generic;
using PropertyChanged;
namespace ee.iLawyer.Ops.Contact.DTO
{
    [AddINotifyPropertyChangedInterface]
    public class Area : ICloneable
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

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
