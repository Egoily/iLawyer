using ee.Framework;
using ee.iLawyer.Ops.Contact.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.Args
{
    public class AddCaseRequest : BaseRequest
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual string CaseCode { get; set; }
        /// <summary>
        /// 主客户
        /// </summary>
        public virtual Client Client { get; set; }

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
    public class UpdateCaseRequest : BaseRequest
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
    public class RemoveCaseRequest : BaseRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual IList<int> Ids { get; set; }
    }
    public class QueryCaseRequest : BasePageRequest
    {
    }

}
