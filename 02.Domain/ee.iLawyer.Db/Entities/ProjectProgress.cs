using System;

namespace ee.iLawyer.Db.Entities
{
    /// <summary>
    /// 项目进展
    /// </summary>
    public class ProjectProgress
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual Project InProject { get; set; }
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

    }
}
