using ee.iLawyer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
namespace ee.iLawyer.ExControls
{
    public class SearchPickBox : TextBox
    {

        public static DependencyProperty WatermarkProperty =
            DependencyProperty.Register(
                "Watermark",
                typeof(string),
                typeof(SearchPickBox));

        public static DependencyProperty WatermarkColorProperty =
            DependencyProperty.Register(
                "WatermarkColor",
                typeof(Brush),
                typeof(SearchPickBox));



        private static DependencyPropertyKey IsNotEmptyKeyPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsNotEmptyKey",
                typeof(bool),
                typeof(SearchPickBox),
                new PropertyMetadata());
        public static DependencyProperty IsNotEmptyKeyProperty = IsNotEmptyKeyPropertyKey.DependencyProperty;


        public static readonly DependencyProperty SearchDataProviderProperty =
    DependencyProperty.Register("SearchDataProvider", typeof(ISearchDataProvider), typeof(SearchPickBox), new UIPropertyMetadata(null));




        public static readonly RoutedEvent SearchEvent =
            EventManager.RegisterRoutedEvent(
                "Search",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(SearchPickBox));


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



        private object selectedObject;
        public object SelectedObject { get => selectedObject; }



        static SearchPickBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(SearchPickBox),
                new FrameworkPropertyMetadata(typeof(SearchPickBox)));
        }

        public SearchPickBox()
            : base()
        {
            Style = (Style)FindResource("SearchPickBoxStyle");
            this.Loaded += SearchBox_Loaded;
            this.OnSearch += SearchPopupBox_OnSearch;
        }

        private void SearchBox_Loaded(object sender, RoutedEventArgs e)
        {
            Observable.FromEventPattern<TextChangedEventArgs>(this, "TextChanged").Throttle(TimeSpan.FromSeconds(1)).Subscribe(x =>
              this.Dispatcher.Invoke(() =>
              {
                  selectedObject = null;
                  RaiseSearchEvent();
              }));
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            IsNotEmptyKey = Text.Length != 0;
            HidePopup();
        }


        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            // if users click on the editing area, the pop up will be hidden
            Type sourceType = e.OriginalSource.GetType();
            if (sourceType != typeof(Image))
            {
                HidePopup();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.MouseLeave += new MouseEventHandler(SearchBox_MouseLeave);
            if (GetTemplateChild("PART_IconBorder") is Border iconBorder)
            {
                iconBorder.MouseLeftButtonDown += new MouseButtonEventHandler(IconBorder_MouseLeftButtonDown);
                iconBorder.MouseLeftButtonUp += new MouseButtonEventHandler(IconBorder_MouseLeftButtonUp);
                iconBorder.MouseLeave += new MouseEventHandler(IconBorder_MouseLeave);
                iconBorder.MouseDown += new MouseButtonEventHandler(SearchIcon_MouseDown);
            }


        }

        private void SearchIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HidePopup();
        }

        private void SearchBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!resultPopup.IsMouseOver)
            {
                HidePopup();
            }
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


        private void IconBorder_MouseLeftButtonDown(object obj, MouseButtonEventArgs e)
        {
            IsMouseLeftButtonDown = true;

        }

        private void IconBorder_MouseLeftButtonUp(object obj, MouseButtonEventArgs e)
        {
            if (!IsMouseLeftButtonDown)
            {
                return;
            }

            if (IsNotEmptyKey)
            {
                this.Text = "";
            }
            RaiseSearchEvent();
            IsMouseLeftButtonDown = false;
        }

        private void IconBorder_MouseLeave(object obj, MouseEventArgs e)
        {
            IsMouseLeftButtonDown = false;

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Text = "";
            }
            else if ((e.Key == Key.Return || e.Key == Key.Enter))
            {
                RaiseSearchEvent();
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        private void RaiseSearchEvent()
        {
    
            SearchEventArgs args = new SearchEventArgs(SearchEvent)
            {
                Keyword = this.Text
            };

            RaiseEvent(args);
        }



        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public Brush WatermarkColor
        {
            get { return (Brush)GetValue(WatermarkColorProperty); }
            set { SetValue(WatermarkColorProperty, value); }
        }

        public bool IsNotEmptyKey
        {
            get { return (bool)GetValue(IsNotEmptyKeyProperty); }
            private set { SetValue(IsNotEmptyKeyPropertyKey, value); }
        }



        public event RoutedEventHandler OnSearch
        {
            add { AddHandler(SearchEvent, value); }
            remove { RemoveHandler(SearchEvent, value); }
        }

        public ISearchDataProvider SearchDataProvider
        {
            get { return (ISearchDataProvider)GetValue(SearchDataProviderProperty); }
            set { SetValue(SearchDataProviderProperty, value); }
        }


        public bool IsMouseLeftButtonDown { get; private set; }

        #region Popup


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
                selectedObject = listBox.SelectedItems[0];
            }
            // close the pop up
            HidePopup();

            this.Focus();
            this.SelectionStart = this.Text?.Length ?? 0;
        }


        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            BuildPopup();
        }


        #endregion
    }




}
