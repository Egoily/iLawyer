using ee.iLawyer.Models;
using ee.iLawyer.Ops.Contact;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.UserControls;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ee.iLawyer.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class HomeViewModel
    {
        public PropertyListItem PhonePropertyListItem { get; set; }
        public int MyProperty { get; set; }

        public ObservableCollection<PropertyListItem> PersonProperties { get; set; }

        public ProjectLevel ProjectLevel { get; set; }

        public MaterialDesignThemes.Wpf.PackIconKind Icon { get; set; }
        public string Text { get; set; }
        public ObservableCollection<Category> CategorySource { get; set; }

        public ObservableCollection<CategoryValue> CategoryValues { get; set; }

        public ClientDataProvider ClientSearchProvider { get { return new ClientDataProvider(); } }
    }


    public class ClientDataProvider : ISearchDataProvider
    {
        public SearchResult DoSearch(string searchTerm)
        {
           return new SearchResult
            {
                SearchTerm = searchTerm,
                Results =!string.IsNullOrEmpty(searchTerm)?
                dict.Where(item => item.Value.ToUpper().Contains(searchTerm.ToUpper())).ToDictionary(v => v.Key, v => v.Value)
                : dict.Take(10).ToDictionary(v => v.Key, v => v.Value)
           };
        }

        public SearchResult SearchByKey(object Key)
        {
            return new SearchResult
            {
                SearchTerm = null,
                Results = dict.Where(item => item.Key.ToString() == Key.ToString()).ToDictionary(v => v.Key, v => v.Value)
            };
        }

        private readonly Dictionary<object, string> dict = new Dictionary<object, string> {
            { 1, "Ego"},
            { 2, "Elise"},
            { 3, "Ego Huang"},
            { 4, "Elise Gao"},
            { 5, "黄广毅"},
            { 6, "高小娜"},
            { 7, "黄一"},
            { 8, "黄二"},
            { 9, "高一"},
            { 10, "高二"},
           
        };
    }
}
