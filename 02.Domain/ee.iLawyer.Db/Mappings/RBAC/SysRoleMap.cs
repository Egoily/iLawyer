using ee.iLawyer.Db.Entities.RBAC;
using FluentNHibernate.Mapping;

namespace ee.iLawyer.Db.Mappings.RBAC
{
    public class SysRoleMap : ClassMap<SysRole>
    {
        public SysRoleMap()
        {
            Table("Sys_Roles");
            LazyLoad();
            Id(x => x.Id);

            //.GeneratedBy.Assigned();
            Map(x => x.Code);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.ParentId).Column("ParentId");


            HasManyToMany(x => x.Users).LazyLoad().ParentKeyColumn("RoleId").ChildKeyColumn("UserId").Table("Sys_UserRole").NotFound.Ignore();
            HasManyToMany(x => x.Permissions).LazyLoad().ParentKeyColumn("RoleId").ChildKeyColumn("ModuleId").Table("Sys_RolePermission").NotFound.Ignore();
            HasManyToMany(x => x.PermissionGroup).LazyLoad().ParentKeyColumn("RoleId").ChildKeyColumn("PermissionGroupId").Table("Sys_RolePermissionGroup").NotFound.Ignore();



        }
    }
}
