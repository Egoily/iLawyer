using ee.Framework.Attributes;
using ee.Framework.Schema;
using ee.iLawyer.Ops.Contact.DTO;
using System.Collections.Generic;

namespace ee.iLawyer.Ops.Contact.Args
{
    public class CreateClientRequest : BaseRequest
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
        public virtual List<CategoryValue> Properties { get; set; }

        /// <summary>
        /// 是否是自然人
        /// </summary>
        public virtual bool IsNP { get; set; }
        /// <summary>
        /// 联系号码
        /// </summary>
        public virtual string ContactNo { get; set; }
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
        /// 联系号码
        /// </summary>
        public virtual string ContactNo { get; set; }
        /// <summary>
        /// 属性列表
        /// </summary>
        [Required]
        public virtual List<CategoryValue> Properties { get; set; }

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
        public virtual int[] Keys { get; set; }
    }

}
