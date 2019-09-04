using ee.Framework.Attributes;
using ee.Framework.Schema;
using System.Collections.Generic;

namespace ee.iLawyer.Ops.Contact.Args
{
    public class CreateJudgeRequest : BaseRequest
    {
        /// <summary>
        /// 法官名
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string ContactNo { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual Gender Gender { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public virtual string Duty { get; set; }
        /// <summary>
        /// 法官等级
        /// </summary>
        public virtual JudgeGrade Grade { get; set; }
        /// <summary>
        /// 所属法院Id
        /// </summary>
        public virtual int InCourtId { get; set; }
    }
    public class UpdateJudgeRequest : BaseRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual int Id { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string ContactNo { get; set; }
        /// <summary>
        /// 法官名
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual Gender Gender { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public virtual string Duty { get; set; }
        /// <summary>
        /// 法官等级
        /// </summary>
        public virtual JudgeGrade Grade { get; set; }
        /// <summary>
        /// 所属法院
        /// </summary>
        public virtual int InCourtId { get; set; }
    }
    public class RemoveJudgeRequest : BaseRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual IList<int> Ids { get; set; }
    }
    public class QueryJudgeRequest : BasePageRequest
    {
        public virtual int[] Keys { get; set; }
        /// <summary>
        /// 法官名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 法官等级
        /// </summary>
        public virtual string Grade { get; set; }
        /// <summary>
        /// 所属法院Id
        /// </summary>
        public virtual int? InCourtId { get; set; }
    }

}
