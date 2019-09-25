using ee.iLawyer.Db.Entities.RBAC;
using System;

namespace ee.iLawyer.Db.Entities
{
    /// <summary>
    /// 文档信息
    /// </summary>
    public class Document
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// 文档类型
        /// </summary>
        public virtual string DocType { get; set; }

        /// <summary>
        /// 文档路径
        /// </summary>
        public virtual string FilePath { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public virtual string Abstract { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public virtual string Labels { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

        /// <summary>
        /// 上传者
        /// </summary>
        public virtual SysUser Uploador { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

    }
}
