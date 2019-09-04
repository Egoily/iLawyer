using System.ComponentModel;
using System.Runtime.Serialization;

namespace ee.iLawyer.Ops.Contact
{
    public enum ProjectLevel
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        Default,
        /// <summary>
        /// 低
        /// </summary>
        [Description("低")]
        Low,
        /// <summary>
        /// 中
        /// </summary>
        [Description("中")]
        Middle,
        /// <summary>
        /// 高
        /// </summary>
        [Description("高")]
        High,

    }
    /// <summary>
    /// 中国法院级别
    /// </summary>
    public enum CourtRank
    {
        /// <summary>
        /// 最高法院
        /// </summary>
        [Description("最高法院")]
        [System.Xml.Serialization.XmlEnum("Supreme")]
        [EnumMember(Value = "Supreme")]
        Supreme,
        /// <summary>
        /// 高等法院
        /// </summary>
        [Description("高等法院")]
        [System.Xml.Serialization.XmlEnum("Superior")]
        [EnumMember(Value = "Superior")]
        Superior,
        /// <summary>
        /// 中级法院
        /// </summary>
        [Description("中级法院")]
        [System.Xml.Serialization.XmlEnum("Intermediate")]
        [EnumMember(Value = "Intermediate")]
        Intermediate,
        /// <summary>
        /// 基层法院
        /// </summary>
        [Description("基层法院")]
        [System.Xml.Serialization.XmlEnum("Grassroots")]
        [EnumMember(Value = "Grassroots")]
        Grassroots,

    }
    /// <summary>
    /// 中国法官等级
    /// </summary>
    public enum JudgeGrade
    {
        [Description("未评级")]
        Undefined = 0,
        [Description("首席大法官")]
        ChiefJustice = 1,
        [Description("一级大法官")]
        FirstClassJustice = 2,
        [Description("二级大法官")]
        SecondTierJustice = 3,
        [Description("一级高级法官")]
        FirstGradeSeniorJudge = 4,
        [Description("二级高级法官")]
        SecondGradeSeniorJudge = 5,
        [Description("三级高级法官")]
        ThreeGradeSeniorJudge = 6,
        [Description("四级高级法官")]
        FourGradeSeniorJudge = 7,
        [Description("一级法官")]
        FirstGradeJudge = 8,
        [Description("二级法官")]
        SecondGradeJudge = 9,
        [Description("三级法官")]
        ThreeGradeJudge = 10,
        [Description("四级法官")]
        FourGradeJudge = 11,
        [Description("五级法官")]
        FiveGradeJudge = 12,
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 未定义
        /// </summary>
        [Description("未定义")]
        Unknown = 0,
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Male = 1,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female = 2,

    }
    /// <summary>
    /// 称呼
    /// </summary>
    public enum Appellation
    {
        /// <summary>
        /// 未定义
        /// </summary>
        [Description(" ")]
        Default = 0,
        /// <summary>
        /// 
        /// </summary>
        [Description("先生")]
        Mr = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("小姐")]
        Miss = 2,
        /// <summary>
        /// 
        /// </summary>
        [Description("女士")]
        Mrs = 3,
    }
    /// <summary>
    /// 待办事项紧急程度
    /// </summary>
    public enum UrgencyDegreeOfTodoItem
    {
        [Description("正常")]
        Normal = 0,
        [Description("紧急")]
        Urgent = 1,
        [Description("特急")]
        ExtraUrgent = 2,
    }
    /// <summary>
    /// 待办事项状态
    /// </summary>
    public enum StatusOfTodoItem
    {
        [Description("待定")]
        Pending = 0,
        [Description("完成")]
        Completed = 1,
        [Description("取消")]
        Canceled = 2,
    }


    public enum MainPrpoertyCategory
    {
        /// <summary>
        /// 电话
        /// </summary>
        [Description("电话")]
        Phone,
        /// <summary>
        /// 邮箱
        /// </summary>
        [Description("邮箱")]
        Email,
        /// <summary>
        /// 地址
        /// </summary>
        [Description("地址")]
        Address,
        /// <summary>
        /// 证件
        /// </summary>
        [Description("证件")]
        Certificate,
        /// <summary>
        /// 重要人物
        /// </summary>
        [Description("重要人物")]
        Person,
        /// <summary>
        /// 重要日期
        /// </summary>
        [Description("重要日期")]
        DateTime,
    }

    public enum RemindTimeType
    {
        /// <summary>
        /// 不提醒
        /// </summary>
        [Description("不提醒")]
        UnRemind,
        /// <summary>
        /// 前一天
        /// </summary>
        [Description("前一天")]
        PreviousOneDay,
        /// <summary>
        /// 同一天
        /// </summary>
        [Description("同一天")]
        CurrentDay,
        /// <summary>
        /// 指定时间
        /// </summary>
        [Description("指定时间")]
        DesignatedDay,
    }
}

