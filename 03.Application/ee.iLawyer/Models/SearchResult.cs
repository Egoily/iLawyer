using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Models
{
    public struct SearchResult
    {
        public string SearchTerm { get; set; }
        public Dictionary<object, string> Results { get; set; }
    }

    public interface ISearchDataProvider
    {
        SearchResult DoSearch(string searchTerm);
        SearchResult SearchByKey(object Key);
    }
}
