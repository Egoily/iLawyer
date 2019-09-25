using System;

namespace ee.iLawyer.Db.Entities
{
    /// <summary>
    /// 待办事项
    /// </summary>
    public class ProjectTodoItem
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 所属项目
        /// </summary>
        public virtual Project InProject { get; set; }
        /// <summary>
        /// 事项名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public virtual int Priority { get; set; }
        /// <summary>
        /// 是否设置提醒
        /// </summary>
        public virtual bool IsSetRemind { get; set; }
        /// <summary>
        /// 提醒时间
        /// </summary>
        public virtual DateTime? RemindTime { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        public virtual DateTime ExpiredTime { get; set; }
        /// <summary>
        /// 事项内容
        /// </summary>
        public virtual string Content { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public virtual DateTime? CompletedTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

    }
}
