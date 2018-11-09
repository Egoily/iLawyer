using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Db.Entity
{
    /// <summary>
    /// 客户属性项
    /// </summary>
    public class ClientPropertyItem
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Client Client { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual PropertyItemCategory Category { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public virtual string Value { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int OrderNo { get; set; }
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
