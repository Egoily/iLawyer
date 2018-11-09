using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Db.Entity
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
        /// 简称
        /// </summary>
        public virtual string Abbreviation { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        public virtual IList<ClientPropertyItem> Properties { get; set; }


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
