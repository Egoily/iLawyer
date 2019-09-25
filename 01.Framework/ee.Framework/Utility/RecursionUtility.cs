using ee.Framework.Schema;
using System.Collections.Generic;
using System.Linq;

namespace ee.Framework.Utility
{
    public static class RecursionUtility
    {

        public static IEnumerable<T> GetChildren<T, TKeyType>(IEnumerable<T> set, T parent)
            where T : RecursionEntity<T, TKeyType>
        {
            if (parent == null || parent.Id == null)
            {
                return null;
            }

            var testquery = from c in set.Where(x => x.ParentId != null)

                            select c;

            var query = from c in set.Where(x => x.ParentId != null)
                        where c.ParentId.Equals(parent.Id)
                        select c;
            if (query == null)
            {
                return null;
            }
            foreach (var item in query.ToList())
            {
                item.Parent = parent;
                item.Children = GetChildren<T, TKeyType>(set, item)?.ToList();
            }
            return query.ToList();
        }
        public static IEnumerable<T> GetTree<T, TKeyType>(IEnumerable<T> set)
             where T : RecursionEntity<T, TKeyType>
        {
            var roots = set.Where(x => x.ParentId == null);
            if (roots?.Any() ?? false)
            {
                foreach (var root in roots)
                {
                    var children = GetChildren<T, TKeyType>(set, root);
                    if (children != null)
                    {
                        root.Children = children.ToList();
                    }
                }
            }
            return roots;
        }

    }
}
