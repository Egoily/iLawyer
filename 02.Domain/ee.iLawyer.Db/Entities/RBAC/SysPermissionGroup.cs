using ee.Framework.Schema;
using System.Collections.Generic;

namespace ee.iLawyer.Db.Entities.RBAC
{
    public class SysPermissionGroup : RecursionEntity<SysPermissionGroup, int?>
    {

        /// <summary>
        ///  名称
        /// </summary>
        public virtual string Name { get; set; }

        public virtual int OrderNo { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }


        public virtual IList<SysModule> Permissions { get; set; }

        public virtual IList<SysRole> Roles { get; set; }
        public virtual IList<SysUser> Users { get; set; }


    }
}
