using ee.Framework.Attributes;
using ee.Framework.Schema;
using ee.iLawyer.Ops.Contact.DTO;
using System;
using System.Collections.Generic;

namespace ee.iLawyer.Ops.Contact.Args
{
    public class CreateProjectRequest : BaseRequest
    {
        /// <summary>
        /// 项目类型代码
        /// </summary>
        [Required]
        public virtual string CategoryCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public virtual string Code { get; set; }
        [Required]
        /// <summary>
        /// 项目等级
        /// </summary>
        public virtual string Level { get; set; }
        /// <summary>
        /// 项目详情
        /// </summary>
        public virtual string Details { get; set; }
        /// <summary>
        /// 收案日期
        /// </summary>
        public virtual DateTime DealDate { get; set; }

        /// <summary>
        /// 关联客户
        /// </summary>
        [Required]
        public virtual IList<int> InvolvedClientIds { get; set; }
        /// <summary>
        /// 其他当事人
        /// </summary>
        public virtual string OtherLitigant { get; set; }
        /// <summary>
        /// 相关方
        /// </summary>
        public virtual string InterestedParty { get; set; }

        /// <summary>
        /// 所属
        /// </summary>
        public virtual int OwnerId { get; set; }
        /// <summary>
        /// 项目帐目
        /// </summary>
        public virtual ProjectAccount Account { get; set; }
        /// <summary>
        /// 待办事项
        /// </summary>
        public virtual IEnumerable<ProjectTodoItem> TodoList { get; set; }
        /// <summary>
        /// 项目进展
        /// </summary>
        public virtual IEnumerable<ProjectProgress> Progresses { get; set; }

    }
    public class UpdateProjectRequest : BaseRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual int Id { get; set; }
        /// <summary>
        /// 项目类别码
        /// </summary>
        [Required]
        public virtual string CategoryCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
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
        /// 关联客户
        /// </summary>
        [Required]
        public virtual IList<int> InvolvedClientIds { get; set; }
        /// <summary>
        /// 其他当事人
        /// </summary>
        public virtual string OtherLitigant { get; set; }
        /// <summary>
        /// 相关方
        /// </summary>
        public virtual string InterestedParty { get; set; }

        /// <summary>
        /// 所属
        /// </summary>
        public virtual int OwnerId { get; set; }
        /// <summary>
        /// 项目帐目
        /// </summary>
        public virtual ProjectAccount Account { get; set; }
        /// <summary>
        /// 待办事项
        /// </summary>
        public virtual IEnumerable<ProjectTodoItem> TodoList { get; set; }
        /// <summary>
        /// 项目进展
        /// </summary>
        public virtual IEnumerable<ProjectProgress> Progresses { get; set; }

    }
    public class RemoveProjectRequest : BaseRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual IList<int> Ids { get; set; }
    }
    public class QueryProjectRequest : BasePageRequest
    {
        public virtual int[] Keys { get; set; }


        /// <summary>
        /// 项目类别码
        /// </summary>
        public virtual string CategoryCode { get; set; }
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
        /// 收案日期(开始)
        /// </summary>
        public virtual string DealDateFrom { get; set; }
        /// <summary>
        /// 收案日期(结束)
        /// </summary>
        public virtual string DealDateTo { get; set; }

        /// <summary>
        /// 所属
        /// </summary>
        public virtual int? OwnerId { get; set; }


    }


    public class GetProjectCategoriesRequest : BaseRequest
    {

    }
    public class GetProjectCausesRequest : BaseRequest
    {

    }
    public class SaveOrUpdateProjectTodoListRequest : BaseRequest
    {
        public virtual int ProjectId { get; set; }
        public virtual IEnumerable<ProjectTodoItem> TodoList { get; set; }
    }
    public class SaveOrUpdateProjectProgressRequest : BaseRequest
    {
        public virtual int ProjectId { get; set; }
        public virtual IEnumerable<ProjectProgress> Progresses { get; set; }
    }
}
