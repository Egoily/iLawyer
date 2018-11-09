using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Db.Entity
{
    /// <summary>
    /// 项目类别
    /// </summary>
    public class ProjectCategory
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }
        public virtual int OrderNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual ProjectCategory Parent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual IList<ProjectCategory> Children { get; set; }
    }
}
