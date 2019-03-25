using System;

namespace ee.iLawyer.Ops.Contact.DTO
{
    /// <summary>
    /// 项目进展
    /// </summary>
    public class ProjectProgress : ICloneable
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public virtual DateTime HandleTime { get; set; }

        /// <summary>
        /// 进展内容
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
