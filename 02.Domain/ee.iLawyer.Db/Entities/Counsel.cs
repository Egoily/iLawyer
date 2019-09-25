using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Db.Entities
{
    /// <summary>
    /// 法律顾问
    /// </summary>
    public class Counsel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// 委托人
        /// </summary>
        public virtual Client Client { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public virtual DateTime StartDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public virtual DateTime EndDate { get; set; }
        /// <summary>
        /// 提供法律服务内容
        /// </summary>
        public virtual string ServiceContent { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

    }
}
