using ee.iLawyer.Models;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace ee.iLawyer.UserControls.Agenda
{
    /// <summary>
    /// Interaction logic for EventDetailPopup.xaml
    /// </summary>
    public partial class EventDetailPopup : Popup
    {


        public Appointment Appointment
        {
            get { return (Appointment)GetValue(AppointmentProperty); }
            set { SetValue(AppointmentProperty, value); }
        }

        public static readonly DependencyProperty AppointmentProperty =
            DependencyProperty.Register("Appointment", typeof(Appointment), typeof(EventDetailPopup), new PropertyMetadata(new Appointment()));

        public EventDetailPopup()
        {
            InitializeComponent();
            DataContext = Appointment;
        }

        private void Close()
        {
            this.IsOpen = false;
            this.StaysOpen = false; 
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }
    }
}
