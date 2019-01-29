using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.DTO
{
    public class Client : ICloneable
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 是否是自然人
        /// </summary>
        public virtual bool IsNP { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public virtual string Abbreviation { get; set; }
        /// <summary>
        /// 印象
        /// </summary>
        public virtual string Impression { get; set; }
        /// <summary>
        /// 属性列表
        /// </summary>
        public virtual List<CategoryValue> Properties { get; set; }


        public Client()
        {
            Properties = new List<CategoryValue>();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
