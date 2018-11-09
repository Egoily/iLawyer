using PropertyChanged;
using System.Windows;
using System.Windows.Controls;

namespace ee.iLawyer.Models
{
    [AddINotifyPropertyChangedInterface]
    public class ModuleItem
    {
        public ModuleItem(string name, object content)
        {
            Name = name;
            Content = content;
            HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto;
            VerticalScrollBarVisibilityRequirement= ScrollBarVisibility.Auto;
        }

        public string Name { get; set; }


        public object Content { get; set; }

        public ScrollBarVisibility HorizontalScrollBarVisibilityRequirement { get; set; }

        public ScrollBarVisibility VerticalScrollBarVisibilityRequirement { get; set; }

        private Thickness _marginRequirement = new Thickness(16);
        public Thickness MarginRequirement
        {
            get { return _marginRequirement; }
            set { _marginRequirement = value; }
        }


    }


}
