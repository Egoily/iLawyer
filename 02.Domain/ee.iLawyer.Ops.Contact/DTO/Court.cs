using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.DTO
{
    /// <summary>
    /// 法院信息
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class Court:ICloneable
    {

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
  
        /// <summary>
        /// 法院名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public CourtRank Rank { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string County { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactNo { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
