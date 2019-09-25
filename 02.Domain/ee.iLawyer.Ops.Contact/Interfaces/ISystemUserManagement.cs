using ee.Framework.Schema;
using ee.iLawyer.Ops.Contact.Args.SystemManagement;
using ee.iLawyer.Ops.Contact.DTO.SystemManagement;

namespace ee.iLawyer.Ops.Contact.Interfaces
{
    public interface ISystemUserManagement
    {
        BaseResponse Register(RegisterRequest request);
        BaseObjectResponse<User> Login(LoginRequest request);
        BaseResponse Logout(LogoutRequest request);

        BaseResponse Grant(GrantRequest request);

        BaseResponse UpdateUser(UpdateUserRequest request);
        BaseResponse ChangePassword(ChangePasswordRequest request);
    }
}
