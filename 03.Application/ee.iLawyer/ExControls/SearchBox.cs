using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
namespace ee.iLawyer.ExControls
{
    public class SearchBox : TextBox
    {

        public static DependencyProperty WatermarkProperty =
            DependencyProperty.Register(
                "Watermark",
                typeof(string),
                typeof(SearchBox));

        public static DependencyProperty WatermarkColorProperty =
            DependencyProperty.Register(
                "WatermarkColor",
                typeof(Brush),
                typeof(SearchBox));



        private static DependencyPropertyKey IsNotEmptyKeyPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsNotEmptyKey",
                typeof(bool),
                typeof(SearchBox),
                new PropertyMetadata());
        public static DependencyProperty IsNotEmptyKeyProperty = IsNotEmptyKeyPropertyKey.DependencyProperty;



        public static readonly RoutedEvent SearchEvent =
            EventManager.RegisterRoutedEvent(
                "Search",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(SearchBox));



        public bool IsShownPreviousItem { get; set; } = true;

        static SearchBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(SearchBox),
                new FrameworkPropertyMetadata(typeof(SearchBox)));
        }

        public SearchBox()
            : base()
        {
            Style = (Style)FindResource("SearchBoxStyle");
            this.Loaded += SearchBox_Loaded;
        }

        private void SearchBox_Loaded(object sender, RoutedEventArgs e)
        {
            Observable.FromEventPattern<TextChangedEventArgs>(this, "TextChanged").Throttle(TimeSpan.FromSeconds(1)).Subscribe(x =>
              this.Dispatcher.Invoke(() =>
              {
                  RaiseSearchEvent();
              }));
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            IsNotEmptyKey = Text.Length != 0;
            HidePopup();
            ShowPreviosItemPopup();
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
            if (GetTemplateChild("PART_SearchIconBorder") is Border iconBorder)
            {
                iconBorder.MouseLeftButtonDown += new MouseButtonEventHandler(IconBorder_MouseLeftButtonDown);
                iconBorder.MouseLeftButtonUp += new MouseButtonEventHandler(IconBorder_MouseLeftButtonUp);
                iconBorder.MouseLeave += new MouseEventHandler(IconBorder_MouseLeave);
                iconBorder.MouseDown += new MouseButtonEventHandler(SearchIcon_MouseDown);
            }


            if (GetTemplateChild("PART_PreviousItem") is Border dropdownIconBorder)
            {
                if (IsShownPreviousItem)
                {
                    dropdownIconBorder.MouseDown += new MouseButtonEventHandler(PreviousItem_MouseDown);
                }
                else
                {
                    dropdownIconBorder.Visibility = Visibility.Hidden;
                }
            }


        }

        private void SearchIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HidePopup();
        }

        private void SearchBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!m_listPopup.IsMouseOver)
            {
                HidePopup();
            }
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
                AddPreviosItem();
                RaiseSearchEvent();
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        private void RaiseSearchEvent()
        {
            if (this.Text == "")
            {
                return;
            }

            SearchEventArgs args = new SearchEventArgs(SearchEvent)
            {
                Keyword = this.Text
            };

            RaiseEvent(args);
        }

        private void AddPreviosItem()
        {

            if (m_listPreviousItem != null && !m_listPreviousItem.Items.Contains(this.Text))
            {
                m_listPreviousItem.Items.Add(this.Text);
            }
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

        public bool IsMouseLeftButtonDown { get; private set; }

        public event RoutedEventHandler OnSearch
        {
            add { AddHandler(SearchEvent, value); }
            remove { RemoveHandler(SearchEvent, value); }
        }

        #region Popup


        private Popup m_listPopup;

        private ListBox m_listPreviousItem = null;

        private void BuildPopup()
        {
            // initialize the pop up
            m_listPopup = new Popup
            {
                PopupAnimation = PopupAnimation.Fade,
                Placement = PlacementMode.Bottom,
                PlacementTarget = this,
                Width = this.ActualWidth
            };

            if (IsShownPreviousItem)
            {
                // initialize the previous items' list
                m_listPreviousItem = new ListBox
                {
                    Width = this.Width,
                    Background =Brushes.White,
                };
                m_listPreviousItem.MouseLeave += new MouseEventHandler(ListPreviousItem_MouseLeave);
                m_listPreviousItem.SelectionChanged += new SelectionChangedEventHandler(ListPreviousItem_SelectionChanged);
            }
        }


        private void ShowPreviosItemPopup()
        {
            if (m_listPreviousItem != null && m_listPreviousItem.Items.Count != 0)
            {
                if (!string.IsNullOrEmpty(this.Text))
                {
                    var listPreviousItem = new ListBox
                    {
                        Width = m_listPreviousItem.Width,
                        Background = m_listPreviousItem.Background,
                    };
                    for (int i = 0; i < m_listPreviousItem.Items.Count; i++)
                    {
                        listPreviousItem.Items.Add(m_listPreviousItem.Items[i]);
                    }
                    listPreviousItem.MouseLeave += new MouseEventHandler(ListPreviousItem_MouseLeave);
                    listPreviousItem.SelectionChanged += new SelectionChangedEventHandler(ListPreviousItem_SelectionChanged);
                    listPreviousItem.Items.Filter += text =>
                    {
                        return (text.ToString().ToLower().StartsWith(this.Text.ToLower()));
                    };
                    if (listPreviousItem.Items.Count > 0)
                    {
                        ShowPopup(listPreviousItem);
                    }
                }
                else
                {
                    if (m_listPreviousItem.Items.Count > 0)
                    {
                        ShowPopup(m_listPreviousItem);
                    }
                }
            }
            else
            {
                HidePopup();
            }
        }
        private void ShowPopup(UIElement item)
        {
            m_listPopup.StaysOpen = true;

            m_listPopup.Child = item;
            m_listPopup.IsOpen = true;

        }
        private void HidePopup()
        {
            m_listPopup.IsOpen = false;
        }
        private void ListPreviousItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void ListPreviousItem_MouseLeave(object sender, MouseEventArgs e)
        {
            // close the pop up
            HidePopup();
        }

        private void PreviousItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowPreviosItemPopup();
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            if (!IsNotEmptyKey)
            {
                this.Watermark = "Search";
            }

            m_listPopup.StaysOpen = false;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            if (!IsNotEmptyKey)
            {
                this.Watermark = "";
            }

            m_listPopup.StaysOpen = true;

        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            BuildPopup();
        }


        #endregion
    }



    public class SearchEventArgs : RoutedEventArgs
    {
        public string Keyword { get; set; } = "";

        public List<string> Sections { get; set; } = new List<string>();
        public SearchEventArgs() : base()
        {

        }
        public SearchEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {

        }
    }
}
