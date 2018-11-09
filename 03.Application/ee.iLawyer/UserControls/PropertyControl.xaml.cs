using ee.iLawyer.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Interaction logic for PropertyPicker.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class PropertyControl : UserControl
    {


        public ObservableCollection<PropertyItem> Items
        {
            get { return (ObservableCollection<PropertyItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<PropertyItem>), typeof(PropertyControl), new PropertyMetadata(OnItemsChanged));



        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(e.Property, e.NewValue);
        }

        private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }


        public PropertyControl()
        {
            InitializeComponent();

            this.DataContext = this;

        }




    }

}
