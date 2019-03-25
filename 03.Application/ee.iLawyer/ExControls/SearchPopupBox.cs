using ee.iLawyer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ee.iLawyer.ExControls
{

    public class SearchPopupBox : SearchBox
    {
        public ISearchDataProvider SearchDataProvider
        {
            get { return (ISearchDataProvider)GetValue(SearchDataProviderProperty); }
            set { SetValue(SearchDataProviderProperty, value); }
        }

        public static readonly DependencyProperty SearchDataProviderProperty =
            DependencyProperty.Register("SearchDataProvider", typeof(ISearchDataProvider), typeof(SearchPopupBox), new UIPropertyMetadata(null));



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

        public ObservableCollection<KeyValuePair<object, string>> SearchResults { get; } = new ObservableCollection<KeyValuePair<object, string>>();



        public SearchPopupBox() : base()
        {
           
            IsShownPreviousItem = false;
            this.OnSearch += SearchPopupBox_OnSearch;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.MouseLeave += new MouseEventHandler(SearchBox_MouseLeave);



        }

        private void SearchPopupBox_OnSearch(object sender, RoutedEventArgs e)
        {
            SearchEventArgs searchArgs = e as SearchEventArgs;
            var result = DoSearch(searchArgs.Keyword);
            if (result.Results == null)
            {
                return;
            }

            SearchResults.Clear();
            result.Results.ToList().ForEach(item => SearchResults.Add((KeyValuePair<object, string>)item));

            resultListBox.ItemsSource = SearchResults;
            resultListBox.SelectedValuePath = "Key";
            resultListBox.DisplayMemberPath = "Value";
            ShowPopup();
        }

        private void SearchBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!resultListBox.IsMouseOver)
            {
                HidePopup();
            }
        }


        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            BuildPopup();
        }

        private Popup resultPopup;

        private ListBox resultListBox = null;
        private void BuildPopup()
        {
            resultPopup = new Popup
            {
                PopupAnimation = PopupAnimation.Fade,
                Placement = PlacementMode.Bottom,
                PlacementTarget = this,
                Width = this.ActualWidth
            };

            resultListBox = new ListBox
            {
                Width = this.Width,
                Background = System.Windows.Media.Brushes.White,
            };

            resultListBox.SelectionChanged += new SelectionChangedEventHandler(resultListBox_SelectionChanged);

        }

        private void HidePopup()
        {
            resultPopup.IsOpen = false;
        }

        private void ShowPopup()
        {
            if (resultListBox.Items.Count != 0)
            {
                resultPopup.StaysOpen = true;

                resultPopup.Child = resultListBox;
                resultPopup.IsOpen = true;
            }
            else
            {
                HidePopup();
            }

        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);


            resultPopup.StaysOpen = false;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            resultPopup.StaysOpen = true;

        }
        private void resultListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            // fetch the previous keyword into the text box
            if (listBox.SelectedItems != null && listBox.SelectedItems.Count > 0)
            {
                this.Text = listBox.SelectedItems[0].ToString();
            }
            // close the pop up
            HidePopup();

            this.Focus();
            this.SelectionStart = this.Text?.Length ?? 0;
        }




    }



}
