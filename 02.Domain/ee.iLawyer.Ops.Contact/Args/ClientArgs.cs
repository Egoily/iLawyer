using ee.Framework;
using ee.iLawyer.Ops.Contact.DTO;
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
        /// 客户名
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 属性列表
        /// </summary>
        [Required]
        public virtual List<KeyValue> Properties { get; set; }

        /// <summary>
        /// 是否是自然人
        /// </summary>
        public virtual bool IsNP { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public virtual string Abbreviation { get; set; }
        /// <summary>
        /// 印象
        /// </summary>
        public virtual string Impression { get; set; }

    }
    public class UpdateClientRequest : BaseRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual int Id { get; set; }
        /// <summary>
        /// 客户名
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 属性列表
        /// </summary>
        [Required]
        public virtual List<KeyValue> Properties { get; set; }

        /// <summary>
        /// 是否是自然人
        /// </summary>
        public virtual bool IsNP { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public virtual string Abbreviation { get; set; }
        /// <summary>
        /// 印象
        /// </summary>
        public virtual string Impression { get; set; }
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
