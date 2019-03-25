using ee.iLawyer.Models;
using ee.iLawyer.Ops.Contact.DTO;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ee.iLawyer.UserControls
{
    /// <summary>
    /// Interaction logic for MultiItemSelector.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class MultiItemSelector : UserControl
    {

        #region  Dependency Properties


        public ISearchDataProvider SearchDataProvider
        {
            get { return (ISearchDataProvider)GetValue(SearchDataProviderProperty); }
            set { SetValue(SearchDataProviderProperty, value); }
        }

        public static readonly DependencyProperty SearchDataProviderProperty =
            DependencyProperty.Register("SearchDataProvider", typeof(ISearchDataProvider), typeof(MultiItemSelector), new UIPropertyMetadata(null));


        public ObservableCollection<MultiItemSelectorItemInfo> SelectedItems
        {
            get { return (ObservableCollection<MultiItemSelectorItemInfo>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(ObservableCollection<MultiItemSelectorItemInfo>), 
                typeof(MultiItemSelector),
                new FrameworkPropertyMetadata(new ObservableCollection<MultiItemSelectorItemInfo>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPropertyChanged));


        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as MultiItemSelector).SelectedItems==null)
            (d as MultiItemSelector).SelectedItems = new ObservableCollection<MultiItemSelectorItemInfo>();
        }


        public int? MaxCountOfSelectedItems
        {
            get { return (int?)GetValue(MaxCountOfSelectedItemsProperty); }
            set { SetValue(MaxCountOfSelectedItemsProperty, value); }
        }
        public static readonly DependencyProperty MaxCountOfSelectedItemsProperty =
            DependencyProperty.Register("MaxCountOfSelectedItems", typeof(int?), typeof(MultiItemSelector), new UIPropertyMetadata(null));




        #endregion


        public ObservableCollection<MultiItemSelectorItemInfo> DataSource { get; } = new ObservableCollection<MultiItemSelectorItemInfo>();


        public bool IsFetchingData { get; set; }




        private void HidePopup()
        {
            if (!popup.IsKeyboardFocusWithin)
            {
                popup.StaysOpen = false;
                popup.IsOpen = false;
            }
        }
        private void ShowPopup()
        {
            if (!popup.IsOpen)
            {
                popup.IsOpen = true;
                popup.StaysOpen = true;
            }
        }
        protected SearchResult DoSearch(string searchTerm)
        {
            if (SearchDataProvider != null)
            {
                int nID = 0;
                SearchResult sr = new SearchResult();
                if (int.TryParse(searchTerm, out nID))
                {
                    sr = SearchDataProvider.SearchByKey(nID);
                }
                else
                {
                    sr = SearchDataProvider.DoSearch(searchTerm);
                }

                return sr;
            }
            else
            {
                return new SearchResult
                {
                    SearchTerm = string.Empty,
                    Results = null
                };

            }

        }
        private void DoOnSearch()
        {
            var result = DoSearch(searchTextBox.Text);
            if (result.Results == null)
            {
                return;
            }

            DataSource.Clear();
            result.Results.ToList().ForEach(item => DataSource.AddIfNotContains((MultiItemSelectorItemInfo)item));

            dataSourceListBox.ItemsSource = DataSource;
            if (DataSource.Count > 0)
            {
                dataSourceListBox.SelectedIndex = 0;
            }

            ShowPopup();
        }






        public void RefreshDataSource()
        {

            var theView = GetDataSourceCollectionView();
            if (theView != null)
            {
                theView.Refresh();
            }
        }

        private bool Select()
        {
            if (MaxCountOfSelectedItems.HasValue && SelectedItems.Count >= MaxCountOfSelectedItems.Value)
            {
                return false;
            }
            bool handled = false;

            MultiItemSelectorItemInfo selectedItem;
            if (popup.IsOpen)
            {
                selectedItem = dataSourceListBox.SelectedItem as MultiItemSelectorItemInfo;
            }
            else
            {
                selectedItem = DataSource.FirstOrDefault(x => x.DisplayText.Equals(searchTextBox.Text, StringComparison.InvariantCultureIgnoreCase));
            }


            if (selectedItem != null)
            {
                SelectedItems.Add(selectedItem);

                searchTextBox.Text = string.Empty;
                searchTextBox.Focus();

                HidePopup();

                handled = true;

                RefreshDataSource();
            }

            return handled;
        }
        private ListCollectionView GetDataSourceCollectionView()
        {
            return (ListCollectionView)CollectionViewSource.GetDefaultView(this.DataSource);
        }
        /// <summary>
        /// 处理按键
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="isPreviewDown"></param>
        /// <returns></returns>
        private bool HandleKeys(Key Key, bool isPreviewDown)
        {
            bool handled = false;
            bool? showPopup = null;
            int? selectedIndex = null;

            switch (Key)
            {
                case Key.Down:
                    if (popup.IsOpen)
                    {
                        selectedIndex = dataSourceListBox.SelectedIndex + 1;
                    }

                    showPopup = true;
                    handled = true;

                    break;
                case Key.Up:
                    if (popup.IsOpen)
                    {
                        selectedIndex = Math.Max(dataSourceListBox.SelectedIndex - 1, 0);
                        showPopup = true;
                        handled = true;
                    }
                    break;

                case Key.F4:
                    if (popup.IsOpen)
                    {
                        showPopup = false;
                    }
                    else
                    {
                        showPopup = true;
                    }
                    handled = true;
                    break;
                case Key.Escape:
                    if (popup.IsOpen)
                    {
                        showPopup = false;
                        handled = true;
                    }
                    break;


            }

            if (showPopup.HasValue == false)
            {
                if (searchTextBox.Text == string.Empty)
                {
                    showPopup = false;
                }
                else
                {
                    var theView = GetDataSourceCollectionView();
                    if (theView == null || theView.Count > 0)
                    {
                        showPopup = true;
                    }
                    else
                    {
                        showPopup = false;
                    }
                }
            }

            if (showPopup.HasValue)
            {
                if (popup.IsOpen == false)
                {
                    selectedIndex = null;
                }

                popup.IsOpen = showPopup.Value;

                if (popup.IsOpen)
                {

                    if (selectedIndex.HasValue)
                    {
                        dataSourceListBox.SelectedIndex = selectedIndex.Value;
                        dataSourceListBox.ScrollIntoView(dataSourceListBox.SelectedItem);
                    }
                    else
                    {
                        dataSourceListBox.SelectedIndex = 0;
                    }
                }
            }

            return handled;
        }















        public MultiItemSelector()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += MultiItemSelector_Loaded;
        }


        private void MultiItemSelector_Loaded(object sender, RoutedEventArgs e)
        {
            Observable.FromEventPattern<TextChangedEventArgs>(searchTextBox, "TextChanged").Throttle(TimeSpan.FromSeconds(1)).Subscribe(x =>
             this.Dispatcher.Invoke(() =>
             {
                 DoOnSearch();
             }));
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            DoOnSearch();
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            HidePopup();
        }

        private void SearchTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Return:
                case Key.Tab:

                    if (!popup.IsOpen)
                    {
                        BindingExpression be = searchTextBox.GetBindingExpression(TextBox.TextProperty);
                        be.UpdateSource();
                        if (be.HasError)
                        {
                            return;
                        }
                    }

                    e.Handled = Select();
                    break;
            }
        }

        private void SearchTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            RefreshDataSource();

            switch (e.Key)
            {
                case Key.Up:
                case Key.Down:
                case Key.Return:
                case Key.Tab:
                    break;
                default:
                    e.Handled = HandleKeys(e.Key, false);
                    break;
            }
        }

        private void SearchTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F5:
                    //TODO:RefreshDataSource here
                    break;
                case Key.Back:
                case Key.Down:
                case Key.Up:
                    e.Handled = HandleKeys(e.Key, true);
                    break;
            }
        }
        private void SearchTextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DoOnSearch();
        }
        private void SearchTextBox_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                HandleKeys(Key.Up, false);
            }
            else if (e.Delta < 0)
            {
                HandleKeys(Key.Down, false);
            }
        }

        private void dataSourceListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Select();
        }

        private void dataSourceListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Focus();
        }

        private void Chip_DeleteClick(object sender, RoutedEventArgs e)
        {
            var item = (sender as MaterialDesignThemes.Wpf.Chip).DataContext as MultiItemSelectorItemInfo;
            SelectedItems.Remove(item);
        }


    }
}
