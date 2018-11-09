using ee.Framework;
using ee.iLawyer.Ops.Contact.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.Args
{
    public class CreateProjectRequest : BaseRequest
    {
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
        public virtual int Level { get; set; }
        /// <summary>
        /// 项目详情
        /// </summary>
        public virtual string Details { get; set; }
        /// <summary>
        /// 关联客户
        /// </summary>
        [Required]
        public virtual IList<Client> Clients { get; set; }

        /// <summary>
        /// 所属
        /// </summary>
        public virtual int OwnerId { get; set; }

    }
    public class UpdateProjectRequest : BaseRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public virtual string CaseCode { get; set; }
        /// <summary>
        /// 主客户
        /// </summary>
        public virtual int ClientId { get; set; }

        /// <summary>
        /// 受理法院
        /// </summary>
        public virtual int CourtId { get; set; }
        /// <summary>
        /// 受理法官
        /// </summary>
        public virtual int JudgeId { get; set; }
        /// <summary>
        /// 主Owner
        /// </summary>
        public virtual int OwnerId { get; set; }

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
    }

}
