using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Db.Entities
{
    /// <summary>
    /// 法院信息
    /// </summary>
    public class Court
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 法院名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public virtual string Rank { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public virtual string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public virtual string City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public virtual string County { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string ContactNo { get; set; }
        /// <summary>
        /// 辖下法官
        /// </summary>
        public virtual IList<Judge> Judges { get; set; }
    }
}
