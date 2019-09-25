using System.Collections.Generic;

namespace ee.iLawyer.Db.Entities.RBAC
{
    public class SysModule
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }


        public virtual SysPermissionGroup InGroup { get; set; }

        public virtual IList<SysRole> Roles { get; set; }
        public virtual IList<SysUser> Users { get; set; }
    }
}
