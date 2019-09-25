using ee.iLawyer.Db.Entities.RBAC;
using FluentNHibernate.Mapping;

namespace ee.iLawyer.Db.Mappings.RBAC
{
    public class SysPermissionGroupMap : ClassMap<SysPermissionGroup>
    {
        public SysPermissionGroupMap()
        {
            Table("Sys_PermissionGroup");
            LazyLoad();

            Id(x => x.Id);
            //.GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.OrderNo);
            Map(x => x.Description);
            Map(x => x.ParentId).Column("ParentId");


            HasMany(x => x.Permissions).Cascade.All().Inverse();

            HasManyToMany(x => x.Roles).LazyLoad().ParentKeyColumn("PermissionGroupId").ChildKeyColumn("RoleId").Table("Sys_RolePermissionGroup").NotFound.Ignore();
            HasManyToMany(x => x.Users).LazyLoad().ParentKeyColumn("PermissionGroupId").ChildKeyColumn("UserId").Table("Sys_UserPermissionGroup").NotFound.Ignore();

        }
    }
}
