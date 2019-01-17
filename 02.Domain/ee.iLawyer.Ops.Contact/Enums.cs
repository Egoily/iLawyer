using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact
{
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
}
