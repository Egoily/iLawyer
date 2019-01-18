using ee.iLawyer.Ops.Contact.DTO;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ee.iLawyer.UserControls
{
    /// <summary>
    /// CategoryTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class CategoryTextBox : UserControl
    {

        public ObservableCollection<Category> CategorySource
        {
            get { return (ObservableCollection<Category>)GetValue(CategorySourceProperty); }
            set { SetValue(CategorySourceProperty, value); }
        }

        public static readonly DependencyProperty CategorySourceProperty =
            DependencyProperty.Register("CategorySource", typeof(ObservableCollection<Category>), typeof(CategoryTextBox),
                new PropertyMetadata(new ObservableCollection<Category>()));

        public CategoryValue CategoryValue
        {
            get { return (CategoryValue)GetValue(CategoryValueProperty); }
            set { SetValue(CategoryValueProperty, value); }
        }
        /// <summary>
        /// 默认为双向绑定依赖属性
        /// </summary>
        public static readonly DependencyProperty CategoryValueProperty =
            DependencyProperty.Register("CategoryValue", typeof(CategoryValue), typeof(CategoryTextBox), 
                new FrameworkPropertyMetadata(new CategoryValue(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //d.SetValue(e.Property, e.NewValue);       
        }


        public CategoryTextBox()
        {
            InitializeComponent();
            DataContext = this;
            this.LostFocus += CategoryTextBox_LostFocus;
        }

        private void CategoryTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!this.popup.IsKeyboardFocusWithin && !this.tvCategories.IsMouseOver)
            {
                this.popup.IsOpen = false;
            }
        }

        private void PathButton_Click(object sender, RoutedEventArgs e)
        {
            if (!popup.IsOpen)
            {
                this.popup.VerticalOffset = grid.ActualHeight;
                this.popup.IsOpen = true;
            }
            else
            {
                this.popup.IsOpen = false;
            }
        }


        private void tvCategories_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.popup.IsKeyboardFocusWithin)
            {

                var selectNode = tvCategories.SelectedItem as Category;

                if (selectNode != null)
                {
                    if (selectNode.Children != null && selectNode.Children.Any())
                    {
                        return;
                    }
                    if(CategoryValue==null)
                    {
                        CategoryValue = new CategoryValue();
                    }
                    CategoryValue.CategoryId = selectNode.Id;
                    CategoryValue.CategoryName = selectNode.Name;
                    txtCategoryName.Text = CategoryValue.CategoryName;
                    MaterialDesignThemes.Wpf.PackIconKind kind = MaterialDesignThemes.Wpf.PackIconKind.Record;
                    bool succ = Enum.TryParse(selectNode.Icon?.ToString(), out kind);
                    icon.Kind = kind;
                }

                this.popup.IsOpen = false;
            }
        }

        private void popup_PreviewMouseDownHandler(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.popup.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (CategoryValue == null)
            {
                CategoryValue = new CategoryValue() { CategoryName = "请选择类型..." };
            }
        }
    }
}
