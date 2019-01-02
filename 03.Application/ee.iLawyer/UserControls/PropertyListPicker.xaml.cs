using ee.iLawyer.Models;
using ee.iLawyer.Ops.Contact.DTO;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ee.iLawyer.UserControls
{
    /// <summary>
    /// Interaction logic for PropertyListPicker.xaml
    /// </summary>
    public partial class PropertyListPicker : UserControl
    {
        public ObservableCollection<PropertyListItem> DataSource
        {
            get { return (ObservableCollection<PropertyListItem>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(ObservableCollection<PropertyListItem>), typeof(PropertyListPicker), new PropertyMetadata(OnPropertyChanged));


        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(e.Property, e.NewValue);
        }


        public PropertyListPicker()
        {
            InitializeComponent();
            this.DataContext = this;


        }

    }
}
