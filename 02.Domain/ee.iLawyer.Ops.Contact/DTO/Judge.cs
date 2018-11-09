using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.DTO
{

    /// <summary>
    /// 法官信息
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class Judge : ICloneable
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string PhoneNo { get; set; }
        /// <summary>
        /// 法官名
        /// </summary>
        public virtual string Name { get; set; }

        private int _gender;
        /// <summary>
        /// 性别
        /// </summary>
        public virtual int Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                GenderString = GetGender(value);
            }
        }
        public virtual string GenderString { get; set; }

        /// <summary>
        /// 所属法院Id
        /// </summary>
        public virtual int InCourtId { get; set; }
        /// <summary>
        /// 所属法院名称
        /// </summary>
        public virtual string InCourtName { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


        public static string GetGender(int gender)
        {
            if (gender == 1) return "男";
            else if (gender == 2) return "女";
            else return "未定义";
        }
    }
}
