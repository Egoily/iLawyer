using ee.iLawyer.Db.Entities.RBAC;
using FluentNHibernate.Mapping;

namespace ee.iLawyer.Db.Mappings.RBAC
{
    public class SysUserMap : ClassMap<SysUser>
    {
        public SysUserMap()
        {
            Table("Sys_Users");
            LazyLoad();

            Id(x => x.Id);
            //.GeneratedBy.Assigned();
            Map(x => x.PhoneNo);
            Map(x => x.UserName);
            Map(x => x.Password);
            Map(x => x.Nickname);
            Map(x => x.Status);
            Map(x => x.CreateTime);
            Map(x => x.UpdateTime);
            Map(x => x.LastLoginTime);
            Map(x => x.NeedResetPwd);

            HasManyToMany(x => x.Roles).LazyLoad().ParentKeyColumn("UserId").ChildKeyColumn("RoleId").Table("Sys_UserRole").NotFound.Ignore();
            HasManyToMany(x => x.Permissions).LazyLoad().ParentKeyColumn("UserId").ChildKeyColumn("ModuleId").Table("Sys_UserPermission").NotFound.Ignore();
            HasManyToMany(x => x.Restrictions).LazyLoad().ParentKeyColumn("UserId").ChildKeyColumn("ModuleId").Table("Sys_UserPermissionRestriction").NotFound.Ignore();
            HasManyToMany(x => x.PermissionGroups).LazyLoad().ParentKeyColumn("UserId").ChildKeyColumn("PermissionGroupId").Table("Sys_UserPermissionGroup").NotFound.Ignore();


        }
    }
}
