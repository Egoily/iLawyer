using ee.iLawyer.Models;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ee.iLawyer.UserControls
{
    /// <summary>
    /// Interaction logic for PropertyPickerControl.xaml
    /// </summary>
    public partial class PropertyPickerControl : UserControl
    {

        public PropertyListItem PropertyListItemOfPhone
        {
            get => (PropertyListItem)GetValue(PropertyListItemOfPhoneProperty);
            set => SetValue(PropertyListItemOfPhoneProperty, value);
        }
        public static readonly DependencyProperty PropertyListItemOfPhoneProperty =
            DependencyProperty.Register("PropertyListItemOfPhone", typeof(PropertyListItem), typeof(PropertyPickerControl), new PropertyMetadata(OnPropertyChanged));



        public PropertyListItem PropertyListItemOfEmail
        {
            get => (PropertyListItem)GetValue(PropertyListItemOfEmailProperty);
            set => SetValue(PropertyListItemOfEmailProperty, value);
        }
        public static readonly DependencyProperty PropertyListItemOfEmailProperty =
            DependencyProperty.Register("PropertyListItemOfEmail", typeof(PropertyListItem), typeof(PropertyPickerControl), new PropertyMetadata(OnPropertyChanged));

        public PropertyListItem PropertyListItemOfAddress
        {
            get => (PropertyListItem)GetValue(PropertyListItemOfAddressProperty);
            set => SetValue(PropertyListItemOfAddressProperty, value);
        }
        public static readonly DependencyProperty PropertyListItemOfAddressProperty =
            DependencyProperty.Register("PropertyListItemOfAddress", typeof(PropertyListItem), typeof(PropertyPickerControl), new PropertyMetadata(OnPropertyChanged));


        public PropertyListItem PropertyListItemOfCertificate
        {
            get => (PropertyListItem)GetValue(PropertyListItemOfCertificateProperty);
            set => SetValue(PropertyListItemOfCertificateProperty, value);
        }
        public static readonly DependencyProperty PropertyListItemOfCertificateProperty =
            DependencyProperty.Register("PropertyListItemOfCertificate", typeof(PropertyListItem), typeof(PropertyPickerControl), new PropertyMetadata(OnPropertyChanged));

        public PropertyListItem PropertyListItemOfPerson
        {
            get => (PropertyListItem)GetValue(PropertyListItemOfPersonProperty);
            set => SetValue(PropertyListItemOfPersonProperty, value);
        }
        public static readonly DependencyProperty PropertyListItemOfPersonProperty =
            DependencyProperty.Register("PropertyListItemOfPerson", typeof(PropertyListItem), typeof(PropertyPickerControl), new PropertyMetadata(OnPropertyChanged));

        public PropertyListItem PropertyListItemOfDateTime
        {
            get => (PropertyListItem)GetValue(PropertyListItemOfDateTimeProperty);
            set => SetValue(PropertyListItemOfDateTimeProperty, value);
        }
        public static readonly DependencyProperty PropertyListItemOfDateTimeProperty =
            DependencyProperty.Register("PropertyListItemOfDateTime", typeof(PropertyListItem), typeof(PropertyPickerControl), new PropertyMetadata(OnPropertyChanged));



        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(e.Property, e.NewValue);
        }


        public PropertyPickerControl()
        {
            InitializeComponent();
            this.DataContext = this;

        }
    }
}
