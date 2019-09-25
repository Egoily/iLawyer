using System.Collections.Generic;

namespace ee.iLawyer.Ops.Contact.DTO
{
    public class PropertyPicker
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Icon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int PickerType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual bool Enabled { get; set; }

        public virtual IList<PropertyPicker> Children { get; set; }
    }
}
