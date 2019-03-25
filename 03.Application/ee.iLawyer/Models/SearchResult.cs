using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Models
{
    public class SearchResult
    {
        public string SearchTerm { get; set; }
        public IList<object> Results { get; set; }
    }

    public interface ISearchDataProvider
    {
        SearchResult DoSearch(string searchTerm);
        SearchResult SearchByKey(object Key);
    }
}
