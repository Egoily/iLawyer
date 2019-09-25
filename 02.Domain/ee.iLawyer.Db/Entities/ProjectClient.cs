using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Db.Entities
{
    /// <summary>
    /// 项目客户信息
    /// </summary>
    public class ProjectClient
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual Project InProject { get; set; }
        /// <summary>
        /// 关联客户
        /// </summary>
        public virtual Client Client { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int OrderNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

    }
}
