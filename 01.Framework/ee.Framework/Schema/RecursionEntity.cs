using System.Collections.Generic;

namespace ee.Framework.Schema
{
    public class RecursionEntity<T, TKeyType>
    {
        public virtual TKeyType Id { get; set; }
        public virtual TKeyType ParentId { get; set; }
        /// <summary>
        /// Parent
        /// </summary>
        public virtual T Parent { get; set; }
        /// <summary>
        /// Children
        /// </summary>
        public virtual IList<T> Children { get; set; }
    }
}
