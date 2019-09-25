using ee.iLawyer.Db.Entities.RBAC;
using FluentNHibernate.Mapping;

namespace ee.iLawyer.Db.Mappings.RBAC
{
    public class SysModuleMap : ClassMap<SysModule>
    {
        public SysModuleMap()
        {
            Table("Sys_Modules");
            LazyLoad();

            Id(x => x.Id);
            //.GeneratedBy.Assigned();
            Map(x => x.Code);
            Map(x => x.Name);
            Map(x => x.Description);
            References(x => x.InGroup).Column("GroupId");

            HasManyToMany(x => x.Roles).LazyLoad().ParentKeyColumn("ModuleId").ChildKeyColumn("RoleId").Table("Sys_RolePermission").NotFound.Ignore();
            HasManyToMany(x => x.Users).LazyLoad().ParentKeyColumn("ModuleId").ChildKeyColumn("UserId").Table("Sys_UserPermission").NotFound.Ignore();


        }
    }
}
