using ee.iLawyer.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ee.iLawyer.UserControls
{
    /// <summary>
    /// Interaction logic for PropertyPicker.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class PropertyPicker : UserControl
    {

        public PropertyListItem PropertyListItem
        {
            get => (PropertyListItem)GetValue(PropertyListItemProperty);
            set => SetValue(PropertyListItemProperty, value);
        }
        public static readonly DependencyProperty PropertyListItemProperty =
            DependencyProperty.Register("PropertyListItem", typeof(PropertyListItem), typeof(PropertyPicker), new PropertyMetadata(OnItemsChanged));


        public bool IsOnlyOne { get; set; }

        public void CorrectItemsProperty()
        {

            if (PropertyListItem != null)
            {
                if (PropertyListItem.Items != null)
                {
                    IsOnlyOne = PropertyListItem.Items.Count < 2;
                    if (PropertyListItem.Items.Any())
                    {
                        for (int i = 0; i < PropertyListItem.Items.Count; i++)
                        {
                            PropertyListItem.Items[i].IsDefault = i == 0;
                            if (PropertyListItem.Items[i].KeyValue == null)
                            {
                                PropertyListItem.Items[i].KeyValue = new KeyValue();
                            }
                        }
                    }
                }
                else
                {
                    PropertyListItem.Items = new ObservableCollection<PropertyPickerItem>()
                    {
                        new PropertyPickerItem()
                        {
                             Guid=Guid.NewGuid(),
                             IsDefault=true,
                             KeyValue=new KeyValue(),
                             OrderNo=0,
                        }
                    };
                }
            }
        }



        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(e.Property, e.NewValue);
        }

        private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PropertyPicker).CorrectItemsProperty();
        }

        public PropertyPicker()
        {

            InitializeComponent();
            IsOnlyOne = true;
            this.DataContext = this;

        }



        private void AddItem()
        {
            var item = new PropertyPickerItem() { Guid = Guid.NewGuid(), IsDefault = false, KeyValue = new KeyValue() };
            PropertyListItem.Items.Add(item);
            IsOnlyOne = false;
        }

        private void RemoveItem(Guid guid)
        {
            var item = PropertyListItem.Items.FirstOrDefault(x => x.Guid == guid);
            if (item != null)
            {
                var isRemoveDefault = item.IsDefault;
                PropertyListItem.Items.Remove(item);
                if (isRemoveDefault)
                {
                    if (PropertyListItem.Items.Any())
                    {
                        PropertyListItem.Items[0].IsDefault = true;
                    }
                }
            }
            CorrectItemsProperty();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddItem();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var guid = (((Button)sender).DataContext as PropertyPickerItem).Guid;
            RemoveItem(guid);

        }

    }

    public enum PickerType
    {
        /// <summary>
        /// 
        /// </summary>
        Default = 0,
        /// <summary>
        /// 
        /// </summary>
        Text = 1,
        /// <summary>
        /// 
        /// </summary>
        DateTime = 2,
    }

    public class Category
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Icon { get; set; }

    }
    public class PickerProperty
    {
        public PickerType PickerType { get; set; }
        public string PropertyName { get; set; }
        public string Icon { get; set; }

        public IList<Category> CategorySource { get; set; }
    }
    [AddINotifyPropertyChangedInterface]
    public class PropertyListItem
    {
        public PickerProperty PickerProperty { get; set; }
        public ObservableCollection<PropertyPickerItem> Items { get; set; }

    }


    public class PickerTypeToVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((PickerType)value == PickerType.DateTime)
                return Visibility.Hidden;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class PickerTypeToHidden : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((PickerType)value == PickerType.DateTime)
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StringToPackIconKind : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MaterialDesignThemes.Wpf.PackIconKind icon = MaterialDesignThemes.Wpf.PackIconKind.Record;
            bool succ = Enum.TryParse(value?.ToString(), out icon);
            return icon;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
