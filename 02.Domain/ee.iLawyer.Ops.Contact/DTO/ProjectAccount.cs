namespace ee.iLawyer.Ops.Contact.DTO
{
    /// <summary>
    /// 帐目
    /// </summary>
    public class ProjectAccount
    {
        /// <summary>
        /// 总金额
        /// </summary>
        public virtual decimal TotalAmount { get; set; }
        /// <summary>
        /// 已收费用
        /// </summary>
        public virtual decimal ReceivedFee { get; set; }
        /// <summary>
        /// 风险比例(百分比)
        /// </summary>
        public virtual decimal RiskBonusPercent { get; set; }
        /// <summary>
        /// 需上交费用
        /// </summary>
        public virtual decimal TurnOverFee { get; set; }
        /// <summary>
        /// 已上交费用
        /// </summary>
        public virtual decimal TurnOverFeePaid { get; set; }
        /// <summary>
        /// 介绍人
        /// </summary>
        public virtual string Introducer { get; set; }
        /// <summary>
        /// 介绍费
        /// </summary>
        public virtual decimal IntroduceFee { get; set; }
        /// <summary>
        /// 已付介绍费
        /// </summary>
        public virtual decimal IntroduceFeePaid { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

    }
}
