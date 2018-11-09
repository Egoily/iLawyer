using FluentNHibernate.Mapping;
using ee.iLawyer.Db.Entity;

namespace ee.iLawyer.Db.Entity.Mapping
{
    public class SysUserMap : ClassMap<SysUser>
    {
        public SysUserMap()
        {
            Table("SysUsers");
            Id(x => x.Id);
            //.GeneratedBy.Assigned();
            Map(x => x.PhoneNo);
            Map(x => x.UserName);
            Map(x => x.Password);
            Map(x => x.Nickname);
            Map(x => x.Status);
            Map(x => x.Level);
            Map(x => x.CreateTime);
            Map(x => x.UpdateTime);
        }
    }
}
