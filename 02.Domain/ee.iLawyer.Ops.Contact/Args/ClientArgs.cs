using ee.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.Args
{
    public class AddClientRequest : BaseRequest
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        public virtual string PhoneNo { get; set; }
        /// <summary>
        /// 客户名
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public virtual string IDCardNo { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        public virtual int Gender { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }
    }
    public class UpdateClientRequest : BaseRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual int Id { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        public virtual string PhoneNo { get; set; }
        /// <summary>
        /// 客户名
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public virtual string IDCardNo { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        public virtual int Gender { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }
    }
    public class RemoveClientRequest : BaseRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual IList<int> Ids { get; set; }
    }
    public class QueryClientRequest : BasePageRequest
    {
    }

}
