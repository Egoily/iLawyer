using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Db.Entity
{
    /// <summary>
    /// 法官信息
    /// </summary>
    public class Judge
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string PhoneNo { get; set; }
        /// <summary>
        /// 法官名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual int Gender { get; set; }
        /// <summary>
        /// 所属法院
        /// </summary>
        public virtual Court InCourt { get; set; }

    }
}
