using System;
using System.Collections.Generic;
using System.Linq;

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
        /// 项目类别
        /// </summary>
        public virtual ProjectCategory Category { get; set; }
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
        public virtual string Level { get; set; }


        /// <summary>
        /// 关联客户列表
        /// </summary>
        public virtual IList<ProjectClient> InvolvedClients { get; set; }
        /// <summary>
        /// 其他当事人
        /// </summary>
        public virtual string OtherLitigant { get; set; }
        /// <summary>
        /// 相关方
        /// </summary>
        public virtual string InterestedParty { get; set; }


        /// <summary>
        /// 项目所属
        /// </summary>
        public virtual SysUser Owner { get; set; }

        /// <summary>
        /// 项目详情
        /// </summary>
        public virtual string Details { get; set; }

        /// <summary>
        /// 收案日期
        /// </summary>
        public virtual DateTime DealDate { get; set; }
        /// <summary>
        /// 联系人/电话
        /// </summary>
        public virtual string Contacts { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 项目费用
        /// </summary>
        public virtual ProjectAccount Account { get; set; }
        /// <summary>
        /// 待办事项
        /// </summary>
        public virtual IList<ProjectTodoItem> TodoList { get; set; }
        /// <summary>
        /// 项目进展
        /// </summary>
        public virtual IList<ProjectProgress> Progresses { get; set; }


        public virtual void AddAccount(ProjectAccount account)
        {
            this.Account = account;
            account.InProject = this;
        }
        public virtual void AddInvolvedClients(IList<ProjectClient> involvedClients)
        {
            this.InvolvedClients = involvedClients;
            if (involvedClients != null && involvedClients.Any())
            {
                foreach (var item in involvedClients)
                {
                    item.InProject = this;
                }
            }
        }

        public virtual void AddTodoList(IList<ProjectTodoItem> todoList)
        {
            this.TodoList = todoList;
            if (todoList != null && todoList.Any())
            {
                foreach (var item in todoList)
                {
                    item.InProject = this;
                }
            }
        }

        public virtual void AddProgresses(IList<ProjectProgress> progresses)
        {
            this.Progresses = progresses;
            if (progresses != null && progresses.Any())
            {
                foreach (var item in progresses)
                {
                    item.InProject = this;
                }
            }
        }
    }
}
