using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Db.Entity
{
    /// <summary>
    /// 项目信息
    /// </summary>
    public class Project
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 项目等级
        /// </summary>
        public virtual int Level { get; set; }
        /// <summary>
        /// 项目详情
        /// </summary>
        public virtual string Details { get; set; }

        /// <summary>
        /// 关联客户列表
        /// </summary>
        public virtual IList<ProjectClient> InvolvedClients { get; set; }


        /// <summary>
        /// 项目所属
        /// </summary>
        public virtual SysUser Owner { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UpdateTime { get; set; }

    }
}
