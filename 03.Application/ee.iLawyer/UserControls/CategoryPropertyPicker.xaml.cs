using ee.iLawyer.Ops.Contact.DTO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ee.iLawyer.UserControls
{
    /// <summary>
    /// Interaction logic for CategoryPropertyPicker.xaml
    /// </summary>
    public partial class CategoryPropertyPicker : UserControl
    {

        public ObservableCollection<Category> CategorySource
        {
            get { return (ObservableCollection<Category>)GetValue(CategorySourceProperty); }
            set { SetValue(CategorySourceProperty, value); }
        }

        public static readonly DependencyProperty CategorySourceProperty =
            DependencyProperty.Register("CategorySource", typeof(ObservableCollection<Category>), typeof(CategoryPropertyPicker), new PropertyMetadata(new ObservableCollection<Category>()));

        public ObservableCollection<CategoryValue> CategoryValues
        {
            get { return (ObservableCollection<CategoryValue>)GetValue(CategoryValuesProperty); }
            set { SetValue(CategoryValuesProperty, value); }
        }
        /// <summary>
        /// 默认为双向绑定依赖属性
        /// </summary>
        public static readonly DependencyProperty CategoryValuesProperty =
            DependencyProperty.Register("CategoryValues", typeof(ObservableCollection<CategoryValue>), typeof(CategoryPropertyPicker),
                new FrameworkPropertyMetadata(new ObservableCollection<CategoryValue>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //d.SetValue(e.Property, e.NewValue);       
        }
      



        public CategoryPropertyPicker()
        {

            InitializeComponent();
            this.DataContext = this;

        }



        private void AddItem()
        {
            CategoryValues.Add(new CategoryValue() { CategoryId = 0, CategoryName = "请选择类型...", Value = string.Empty });
        }

        private void RemoveItem(CategoryValue item)
        {
            //var item = categoryValue.GetHashCode();
            CategoryValues.Remove(item);
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddItem();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button).DataContext as CategoryValue;
            RemoveItem(item);
        }

    }



}
