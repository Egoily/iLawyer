using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ee.iLawyer.UserControls
{
    /// <summary>
    /// Interaction logic for TCtr.xaml
    /// </summary>
    public partial class TCtr : UserControl
    {

        public PropertyListItem PropertyListItem
        {
            get => (PropertyListItem)GetValue(PropertyListItemProperty);
            set => SetValue(PropertyListItemProperty, value);
        }
        public static readonly DependencyProperty PropertyListItemProperty =
            DependencyProperty.Register("PropertyListItem", typeof(PropertyListItem), typeof(TCtr), new PropertyMetadata(OnPropertyChanged));


        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }
        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.Register("PropertyName", typeof(string), typeof(TCtr), new PropertyMetadata(OnPropertyChanged));


        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(TCtr), new PropertyMetadata(OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(e.Property, e.NewValue);
        }

        public TCtr()
        {
            InitializeComponent();
            this.DataContext = this;

        }
    }
}
