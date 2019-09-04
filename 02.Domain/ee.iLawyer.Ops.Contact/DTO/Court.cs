using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.DTO
{
    /// <summary>
    /// 法院信息
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    [DataContract]
    public class Court:ICloneable
    {

        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 法院名
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        [DataMember]
        public CourtRank Rank { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        [DataMember]
        public string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        [DataMember]
        public string City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        [DataMember]
        public string County { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [DataMember]
        public string Address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [DataMember]
        public string ContactNo { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
