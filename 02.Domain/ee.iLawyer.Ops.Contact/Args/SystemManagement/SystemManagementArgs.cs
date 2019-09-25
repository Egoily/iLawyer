using ee.Framework.Schema;
using System.Collections.Generic;

namespace ee.iLawyer.Ops.Contact.Args.SystemManagement
{

    public class RegisterRequest : BaseRequest
    {
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }

        public virtual string PhoneNo { get; set; }
        public virtual IEnumerable<int> RoleIds { get; set; }
    }

    public class LoginRequest : BaseRequest
    {
        public virtual string LoginName { get; set; }
        public virtual string Password { get; set; }
    }

    public class LogoutRequest : BaseRequest
    {
        public virtual int UserId { get; set; }
    }
    public class GrantRequest : BaseRequest
    {
        public virtual OperatePattern Pattern { get; set; }
        public virtual int UserId { get; set; }
        public virtual IEnumerable<int> RoleIds { get; set; }
        public virtual IEnumerable<int> PermissionIds { get; set; }
        public virtual IEnumerable<int> PermissionGroupIds { get; set; }
        public virtual IEnumerable<int> PermissionRestrictionIds { get; set; }
    }

    public class UpdateUserRequest : BaseRequest
    {
        public virtual int UserId { get; set; }
        public virtual string NickName { get; set; }
        public virtual int? Status { get; set; }
    }
    public class ChangePasswordRequest : BaseRequest
    {
        public virtual int UserId { get; set; }
        public virtual string OldPassword { get; set; }
        public virtual string NewPassword { get; set; }
    }

}
