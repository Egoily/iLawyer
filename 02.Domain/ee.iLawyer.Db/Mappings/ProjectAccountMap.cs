﻿using ee.iLawyer.Db.Entities;
using FluentNHibernate.Mapping;
namespace ee.iLawyer.Db.Mappings
{
    public class ProjectAccountMap : ClassMap<ProjectAccount>
    {
        public ProjectAccountMap()
        {
            Table("ProjectAccounts");
            LazyLoad();

            Id(x => x.Id).GeneratedBy.Foreign("InProject");
            //.GeneratedBy.Assigned();

            HasOne(x => x.InProject).Cascade.None().Constrained();

            Map(x => x.TotalAmount);
            Map(x => x.RiskBonusPercent);
            Map(x => x.ReceivedFee);
            Map(x => x.TurnOverFee);
            Map(x => x.TurnOverFeePaid);
            Map(x => x.Introducer);
            Map(x => x.IntroduceFee);
            Map(x => x.IntroduceFeePaid);
            Map(x => x.Remark);
        }
    }
}
