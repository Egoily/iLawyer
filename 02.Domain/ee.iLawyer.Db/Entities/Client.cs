using System;
using System.Collections.Generic;

namespace ee.iLawyer.Db.Entities
{
    /// <summary>
    /// 客户信息
    /// </summary>
    public class Client
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
        /// <summary>
        /// 属性
        /// </summary>
        public virtual IList<ClientProperties> Properties { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? UpdateTime { get; set; }


    }
}
