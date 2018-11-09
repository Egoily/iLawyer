﻿using ee.iLawyer.Models;
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
    public partial class PropertyPicker : UserControl
    {
        public Dictionary<int, string> CategorySource
        {
            get { return (Dictionary<int, string>)GetValue(CategorySourceProperty); }
            set { SetValue(CategorySourceProperty, value); }
        }
        public static readonly DependencyProperty CategorySourceProperty =
         DependencyProperty.Register("CategorySource", typeof(Dictionary<int, string>), typeof(PropertyPicker), new PropertyMetadata(OnPropertyChanged));

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(string), typeof(PropertyPicker), new PropertyMetadata(OnPropertyChanged));

        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }

        public static readonly DependencyProperty PropertyNameProperty =
      DependencyProperty.Register("PropertyName", typeof(string), typeof(PropertyPicker), new PropertyMetadata(OnPropertyChanged));



        public PickerType PickerType
        {
            get { return (PickerType)GetValue(PickerTypeProperty); }
            set { SetValue(PickerTypeProperty, value); }
        }

        public static readonly DependencyProperty PickerTypeProperty =
            DependencyProperty.Register("PickerType", typeof(PickerType), typeof(PropertyPicker), new PropertyMetadata(OnPropertyChanged));


        public ObservableCollection<PropertyPickerItem> Items
        {
            get { return (ObservableCollection<PropertyPickerItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<PropertyPickerItem>), typeof(PropertyPicker), new PropertyMetadata(OnItemsChanged));



        public bool IsOnlyOne { get; set; }

        public void SetIsOnlyoneProperty()
        {
            if (Items != null) 
            IsOnlyOne = Items.Count < 2;
        }




        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(e.Property, e.NewValue);
        }

        private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PropertyPicker).SetIsOnlyoneProperty();
        }



       


        public PropertyPicker()
        {
            InitializeComponent();

            this.DataContext = this;
            if (Items == null)
            {
                Items = new ObservableCollection<PropertyPickerItem>();
                Items.Add(new PropertyPickerItem() { Guid = Guid.NewGuid(), IsDefault = true });
                IsOnlyOne = true;
            }
        }



        private void AddItem()
        {

            var item = new PropertyPickerItem() { Guid = Guid.NewGuid(), IsDefault = false };
            Items.Add(item);
            IsOnlyOne = false;
        }

        private void RemoveItem(Guid guid)
        {
            var item = Items.FirstOrDefault(x => x.Guid == guid);
            if (item != null)
            {
                var isRemoveDefault = item.IsDefault;
                Items.Remove(item);
                if (isRemoveDefault)
                {
                    Items[0].IsDefault = true;
                }
            }
            SetIsOnlyoneProperty();
        }


        private void btnOpera_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag.ToString() == "Add")
            {
                AddItem();
            }
            else
            {
                var guid = (((Button)sender).DataContext as PropertyPickerItem).Guid;
                RemoveItem(guid);
            }
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
        public T FindFirstVisualChild<T>(DependencyObject obj, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T && child.GetValue(NameProperty).ToString() == childName)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindFirstVisualChild<T>(child, childName);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }

        public List<T> GetChildObjects<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();
            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);
                if (child is T && (((T)child).Name == name || string.IsNullOrEmpty(name)))
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child, ""));//指定集合的元素添加到List队尾
            }
            return childList;
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
