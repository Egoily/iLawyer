using ee.iLawyer.Domain;
using ee.iLawyer.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ee.iLawyer.UserControls.Agenda
{
    /// <summary>
    /// Interaction logic for DayBoxControl.xaml
    /// </summary>
    public partial class DayBoxControl : UserControl
    {

        public DateTime BindingDate
        {
            get { return (DateTime)GetValue(BindingDateProperty); }
            set { SetValue(BindingDateProperty, value); }
        }

        public static readonly DependencyProperty BindingDateProperty =
            DependencyProperty.Register("BindingDate", typeof(DateTime), typeof(DayBoxControl), new PropertyMetadata(OnBindingDatePropertyChanged));

        public ObservableCollection<Appointment> Appointments
        {
            get { return (ObservableCollection<Appointment>)GetValue(AppointmentsProperty); }
            set { SetValue(AppointmentsProperty, value); }
        }

        public static readonly DependencyProperty AppointmentsProperty =
            DependencyProperty.Register("Appointments", typeof(ObservableCollection<Appointment>), typeof(DayBoxControl),
                  new FrameworkPropertyMetadata(new ObservableCollection<Appointment>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnAppointmentsPropertyChanged));





        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(DayBoxControl), new PropertyMetadata(OnIsSelectePropertyChanged));





        public static readonly RoutedEvent SelectionClickEvent =
            EventManager.RegisterRoutedEvent("SelectionClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DayBoxControl));



        /// <summary>
        /// Add / Remove SelectionClick handler 
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler OnSelectionClick
        {
            add { AddHandler(SelectionClickEvent, value); }
            remove { RemoveHandler(SelectionClickEvent, value); }
        }


        public delegate void AppointmentClickEventHandler(object sender, ClickAppointmentRoutedEventArgs e);

        public static readonly RoutedEvent AppointmentClickEvent =
            EventManager.RegisterRoutedEvent("AppointmentClick", RoutingStrategy.Bubble, typeof(AppointmentClickEventHandler), typeof(DayBoxControl));


        /// <summary>
        /// Add / Remove AppointmentClick handler 
        /// </summary>
        [Category("Behavior")]
        public event AppointmentClickEventHandler OnAppointmentClick
        {
            add { AddHandler(AppointmentClickEvent, value); }
            remove { RemoveHandler(AppointmentClickEvent, value); }
        }




        private static void OnAppointmentsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(e.Property, e.NewValue);
            (d as DayBoxControl).InitAppointments();
        }


        private static void OnBindingDatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(e.Property, e.NewValue);
            (d as DayBoxControl).SetText();

        }

        private static void OnIsSelectePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(e.Property, e.NewValue);
            (d as DayBoxControl).EffectInSelection();
        }

        public void InitAppointments()
        {
            if (Appointments == null)
            {
                Appointments = new ObservableCollection<Appointment>();
            }
        }
        public void AddAppointment(Appointment apt)
        {
            InitAppointments();
            if (apt != null)
            {
                Appointments.Add(apt);
            }
        }
        public void RemoveAppointment(Appointment apt)
        {
            InitAppointments();
            if (apt != null)
            {
                if (Appointments.Contains(apt))
                {
                    Appointments.Remove(apt);
                }
            }
        }
        public void SetText()
        {
            if (BindingDate == DateTime.Today)
            {
                txtTopLeft.FontSize = 12;
                txtTopLeft.Text = $"今日({BindingDate.ToString("M月d日")})";
                DayLabelRowBorder.Background = (Brush)TryFindResource("OrangeGradientBrush");
            }
            else
            {
                txtTopLeft.FontSize = 20;
                txtTopLeft.Text = BindingDate.Day.ToString();
                DayLabelRowBorder.Background = (Brush)TryFindResource("BlueGradientBrush");

            }

            var lunarClendar = new ChineseLunisolarCalendarWithFestival(BindingDate);//获取农历日期
            if (!string.IsNullOrEmpty(lunarClendar.FestivalString))
            {
                txtTopRight.FontSize = 10;
                txtTopRight.Text = lunarClendar.FestivalString;
            }
            else
            {
                txtTopRight.FontSize = 16;
                txtTopRight.Text = lunarClendar.LunarDayValue;
            }
        }
        public void EffectInSelection()
        {
            if (IsSelected)
            {
                mainBorder.BorderThickness = new Thickness(_selectedBorderThickness);
                mainBorder.BorderBrush = Brushes.LightGreen;
                popupNewEventControl.IsOpen = true;
                popupNewEventControl.StaysOpen = true;
            }
            else
            {
                mainBorder.BorderThickness = new Thickness(_defaultBorderThickness);
                mainBorder.BorderBrush = Brushes.CadetBlue;

                if (popupNewEventControl.IsOpen)
                {
                    popupNewEventControl.IsOpen = false;
                    popupNewEventControl.StaysOpen = false;
                }
            }
        }

        public void Ashing(bool flag = false)
        {
            if (flag)
            {
                txtTopLeft.Foreground = Brushes.Gray;
                txtTopRight.Foreground = Brushes.Gray;
                txtContent.Foreground = Brushes.Gray;
            }
            else
            {
                txtTopLeft.Foreground = Brushes.Black;
                txtTopRight.Foreground = Brushes.Black;
                txtContent.Foreground = Brushes.Black;
            }
        }


        private int _defaultBorderThickness = 1;
        private int _selectedBorderThickness = 2;
        public DayBoxControl()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitAppointments();
        }

        private void UserControl_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var args = new RoutedEventArgs(SelectionClickEvent, this);
            RaiseEvent(args);
            IsSelected = true;
        }

        private void NewEvent_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("Popup new event frame");
        }

        private void Grid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (IsSelected)
            {
                popupNewEventControl.IsOpen = true;
                popupNewEventControl.StaysOpen = true;
            }
        }

        private void Grid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!popupNewEventControl.IsKeyboardFocusWithin && !popupNewEventControl.IsMouseOver)
            {
                popupNewEventControl.IsOpen = false;
                popupNewEventControl.StaysOpen = false;
            }
        }

        private void DayBoxAppointmentControl_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var args = new ClickAppointmentRoutedEventArgs(AppointmentClickEvent, this, ((DayBoxAppointmentControl)sender).DataContext as Appointment);
            RaiseEvent(args);
        }
    }

    public class ClickAppointmentRoutedEventArgs : RoutedEventArgs
    {
        public Appointment Appointment { get; set; }
        public ClickAppointmentRoutedEventArgs(RoutedEvent routedEvent, object source)
            : base(routedEvent, source)
        {
        }
        public ClickAppointmentRoutedEventArgs(RoutedEvent routedEvent, object source, Appointment appointment)
            : base(routedEvent, source)
        {
            Appointment = appointment;
        }

    }
}
