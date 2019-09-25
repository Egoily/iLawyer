using ee.Framework.Schema;

namespace ee.iLawyer.Db.Entities.Foundation
{

    public class Picklist : RecursionEntity<Picklist, int?>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string Category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string SubValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual bool Enabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int OrderNo { get; set; }




        //public virtual int ParentId { get; set; }

        //public virtual Picklist Parent { get; set; }

        //public virtual IList<Picklist> Children { get; set; }
    }
}
