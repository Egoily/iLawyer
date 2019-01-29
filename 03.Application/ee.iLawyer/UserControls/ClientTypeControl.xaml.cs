using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ee.iLawyer.UserControls
{
    /// <summary>
    /// ClientTypeControl.xaml 的交互逻辑
    /// </summary>
    public partial class ClientTypeControl : UserControl
    {


        //自定义事件委托 
        public delegate void TypeRoutedEventHandler(object sender, TypeRoutedEventArge e);


        public bool IsNaturalPerson
        {
            get { return (bool)GetValue(IsNaturalPersonProperty); }
            set { SetValue(IsNaturalPersonProperty, value); }
        }

        public static readonly DependencyProperty IsNaturalPersonProperty =
            DependencyProperty.Register("IsNaturalPerson", typeof(bool), typeof(ClientTypeControl), new PropertyMetadata(false, OnPropertyChanged));




        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(e.Property, e.NewValue);
            (d as ClientTypeControl).SetSelected();
        }

        public void SetSelected()
        {

            if (IsNaturalPerson)
            {
                orgBorder.Background = Brushes.Transparent;
                npnBorder.Background = Brushes.Orange;
            }
            else
            {
                orgBorder.Background = Brushes.Orange;
                npnBorder.Background = Brushes.Transparent;

            }
        }





        public event TypeRoutedEventHandler TypeChanged
        {
            add { AddHandler(TypeChangedEvent, value); }
            remove { RemoveHandler(TypeChangedEvent, value); }
        }
        public static readonly RoutedEvent TypeChangedEvent =
            EventManager.RegisterRoutedEvent("TypeChanged", RoutingStrategy.Bubble, typeof(TypeRoutedEventHandler), typeof(ClientTypeControl));







        public ClientTypeControl()
        {
            InitializeComponent();
        }


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabled)
            {
                return;
            }

            orgBorder.Background = Brushes.Transparent;
            npnBorder.Background = Brushes.Transparent;
            (sender as Border).Background = Brushes.Orange;
            var isNP = ((sender as Border).Name == "npnBorder");
            if (isNP != IsNaturalPerson)
            {
                IsNaturalPerson = isNP;
                var args = new TypeRoutedEventArge
                {
                    RoutedEvent = TypeChangedEvent,
                    IsNaturalPerson = isNP,
                };
                RaiseEvent(args);
            }
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IsEnabled)
            {
                (sender as Border).Cursor = Cursors.Hand;
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (IsEnabled)
            {
                (sender as Border).Cursor = Cursors.None;
            }
        }
    }
    /// <summary> 
    /// 自定义事件参数类 
    /// </summary> 
    public class TypeRoutedEventArge : RoutedEventArgs
    {
        public bool IsNaturalPerson { get; set; }
    }

}
