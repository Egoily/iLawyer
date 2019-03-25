namespace ee.iLawyer.Db.Entity
{
    /// <summary>
    /// 法官信息
    /// </summary>
    public class Judge
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string ContactNo { get; set; }
        /// <summary>
        /// 法官名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual int Gender { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public virtual string Duty { get; set; }
        /// <summary>
        /// 法官等级
        /// </summary>
        public virtual string Grade { get; set; }
        /// <summary>
        /// 所属法院
        /// </summary>
        public virtual Court InCourt { get; set; }

    }
}
