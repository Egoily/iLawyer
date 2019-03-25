using FluentNHibernate.Mapping;
namespace ee.iLawyer.Db.Entity.Mapping
{
    public class ProjectAccountMap : ClassMap<ProjectAccount>
    {
        public ProjectAccountMap()
        {
            Table("ProjectAccounts");
            LazyLoad();
            Id(x => x.Id).GeneratedBy.Foreign("Project");
            //.GeneratedBy.Assigned();
            Map(x => x.TotalAmount);
            Map(x => x.RiskBonusPercent);
            Map(x => x.ReceivedFee);
            Map(x => x.TurnOverFee);
            Map(x => x.TurnOverFeePaid);
            Map(x => x.Introducer);
            Map(x => x.IntroduceFee);
            Map(x => x.IntroduceFeePaid);
            Map(x => x.Remark);
            HasOne(x => x.InProject).Cascade.All();
        }
    }
}
