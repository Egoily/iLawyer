using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.DTO
{
    public class Project : ICloneable
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
        public virtual ProjectLevel Level { get; set; }
        /// <summary>
        /// 项目详情
        /// </summary>
        public virtual string Details { get; set; }

        /// <summary>
        /// 干系人
        /// </summary>
 
        /// <summary>
        /// 项目所属
        /// </summary>


        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UpdateTime { get; set; }

        public Project()
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
