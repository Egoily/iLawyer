using System;

namespace ee.iLawyer.Db.Entities
{
    /// <summary>
    /// 客户属性项
    /// </summary>
    public class ClientProperties
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
        public virtual Foundation.Picklist Picker { get; set; }
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
