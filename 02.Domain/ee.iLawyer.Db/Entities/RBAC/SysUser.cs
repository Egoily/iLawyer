using System;
using System.Collections.Generic;

namespace ee.iLawyer.Db.Entities.RBAC
{
    /// <summary>
    /// 系统用户信息
    /// </summary>
    public class SysUser
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string PhoneNo { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public virtual string Nickname { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? UpdateTime { get; set; }
        public virtual DateTime? LastLoginTime { get; set; }
        public virtual bool NeedResetPwd { get; set; }


        public virtual IList<SysRole> Roles { get; set; }

        public virtual IList<SysModule> Permissions { get; set; }
        public virtual IList<SysModule> Restrictions { get; set; }
        public virtual IList<SysPermissionGroup> PermissionGroups { get; set; }
    }
}
