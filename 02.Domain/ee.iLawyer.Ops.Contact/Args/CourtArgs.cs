using ee.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.Args
{
    public class AddCourtRequest : BaseRequest
    {
        /// <summary>
        /// 法院名
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        [Required]
        public virtual string Rank { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        //[Required]
        public virtual string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        //[Required]
        public virtual string City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        //[Required]
        public virtual string County { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Required]
        public virtual string Address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string ContactNo { get; set; }

    }
    public class UpdateCourtRequest : BaseRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual int Id { get; set; }
        /// <summary>
        /// 法院名
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        [Required]
        public virtual string Rank { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        [Required]
        public virtual string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        [Required]
        public virtual string City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        [Required]
        public virtual string County { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Required]
        public virtual string Address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string ContactNo { get; set; }

    }
    public class RemoveCourtRequest : BaseRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual IList<int> Ids { get; set; }
    }
    public class QueryCourtRequest : BasePageRequest
    {
    }

}
