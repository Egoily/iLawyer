using ee.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.Args
{
    public class AddJudgeRequest : BaseRequest
    {
        /// <summary>
        /// 法官名
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string PhoneNo { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual int Gender { get; set; }
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
        /// 手机号码
        /// </summary>
        public virtual string PhoneNo { get; set; }
        /// <summary>
        /// 法官名
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual int Gender { get; set; }
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
    }

}
