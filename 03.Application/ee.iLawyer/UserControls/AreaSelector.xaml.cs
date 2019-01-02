using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ee.iLawyer.Ops.Contact.DTO;
namespace ee.iLawyer.UserControls
{
    /// <summary>
    /// Interaction logic for AreaSelector.xaml
    /// </summary>
    public partial class AreaSelector : UserControl, INotifyPropertyChanged
    {

        private IList<Area> _provinces = new List<Area>();

        public static readonly DependencyProperty ProvinceProperty = DependencyProperty.Register("Province", typeof(string), typeof(AreaSelector), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty CityProperty = DependencyProperty.Register("City", typeof(string), typeof(AreaSelector), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty CountyProperty = DependencyProperty.Register("County", typeof(string), typeof(AreaSelector), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty ProvinceCodeProperty = DependencyProperty.Register("ProvinceCode", typeof(string), typeof(AreaSelector), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty CityCodeProperty = DependencyProperty.Register("CityCode", typeof(string), typeof(AreaSelector), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty CountyCodeProperty = DependencyProperty.Register("CountyCode", typeof(string), typeof(AreaSelector), new FrameworkPropertyMetadata(string.Empty));



        public IList<Area> Provinces
        {
            get
            {
                return _provinces;
            }
            set
            {
                _provinces.Clear();
                if (value != null)
                {
                    foreach (var province in value)
                    {
                        _provinces.Add(province);
                    }
                }
            }
        }

        public AreaSelector()
        {
            InitializeComponent();
            this.grid.DataContext = this;
            this.PropertyChanged += SelectedArea_PropertyChanged;
        }


        private void SelectedArea_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(Province + " " + City + " " + County);
        }


        private string _province;
        private string _city;
        private string _county;

        private string _provinceCode;
        private string _cityCode;
        private string _countyCode;

        public string Province
        {
            get { return _province; }
            set
            {
                _province = value;
                _city = null;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Province"));
                }
            }
        }
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                _county = null;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("City"));
                }
            }
        }
        public string County
        {
            get { return _county; }
            set
            {
                _county = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("County"));
                }
            }
        }


        public string ProvinceCode
        {
            get { return _provinceCode; }
            set
            {
                _provinceCode = value;
                _cityCode = string.Empty;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ProvinceCode"));
                }
            }
        }
        public string CityCode
        {
            get { return _cityCode; }
            set
            {
                _cityCode = value;
                _countyCode = string.Empty;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CityCode"));
                }
            }
        }
        public string CountyCode
        {
            get { return _countyCode; }
            set
            {
                _countyCode = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CountyCode"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }



}
