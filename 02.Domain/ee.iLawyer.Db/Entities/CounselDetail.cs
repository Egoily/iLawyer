using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Db.Entities
{
    /// <summary>
    /// 法律顾问详情
    /// </summary>
    public class CounselDetail
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// 顾问
        /// </summary>
        public virtual Counsel Counsel { get; set; }
        /// <summary>
        /// 服务内容
        /// </summary>
        public virtual string Content { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public virtual DateTime StartDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public virtual DateTime EndDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

    }
}
