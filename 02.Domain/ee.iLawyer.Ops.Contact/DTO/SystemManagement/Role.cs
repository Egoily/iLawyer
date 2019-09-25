using System.Collections.Generic;

namespace ee.iLawyer.Ops.Contact.DTO.SystemManagement
{
    public class Role
    {

        public virtual int Id { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        ///  名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        public virtual Role Parent { get; set; }
        public virtual IList<Role> Children { get; set; }

    }
}
