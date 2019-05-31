
using ee.iLawyer.Domain;
using ee.iLawyer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace ee.iLawyer.UserControls.Agenda
{

    public partial class MonthView : UserControl
    {
        private Brush _dayBackBrush = Brushes.White;
        private Brush _todayBackBrush;
        private Brush _targetBackBrush = Brushes.LightSlateGray;

        private static DateTime _DisplayStartDate = DateTime.Now.AddDays(-1 * (DateTime.Now.Day - 1));
        private int _DisplayMonth = _DisplayStartDate.Month;
        private int _DisplayYear = _DisplayStartDate.Year;
        private static CultureInfo _cultureInfo = new CultureInfo(CultureInfo.CurrentUICulture.LCID);
        private System.Globalization.Calendar sysCal = _cultureInfo.Calendar;
        private List<Appointment> _monthAppointments;

        public event DisplayMonthChangedEventHandler DisplayMonthChanged;

        public delegate void DisplayMonthChangedEventHandler(MonthChangedEventArgs e);

        public event DayBoxDoubleClickedEventHandler DayBoxDoubleClicked;

        public delegate void DayBoxDoubleClickedEventHandler(NewAppointmentEventArgs e);

        public event AppointmentDblClickedEventHandler AppointmentDblClicked;

        public delegate void AppointmentDblClickedEventHandler(int Appointment_Id);

        public event AppointmentMovedEventHandler AppointmentMoved;

        public delegate void AppointmentMovedEventHandler(int Appointment_Id, int OldDay, int NewDay);

        public DateTime DisplayStartDate
        {
            get
            {
                return _DisplayStartDate;
            }
            set
            {
                _DisplayStartDate = value;
                _DisplayMonth = _DisplayStartDate.Month;
                _DisplayYear = _DisplayStartDate.Year;
            }
        }

        public List<Appointment> MonthAppointments
        {
            get
            {
                return _monthAppointments;
            }
            set
            {
                _monthAppointments = value;
                BuildCalendarUI();
            }
        }

        public MonthView()
        {
            InitializeComponent();
            _todayBackBrush = (Brush)TryFindResource("OrangeGradientBrush");
        }
        private void MonthView_Loaded(object sender, RoutedEventArgs e)
        {
            // -- Want to have the calendar show up, even if no appoints are assigned 
            // Note - in my own app, appointments are loaded by a backgroundWorker thread to avoid a laggy UI
            if (_monthAppointments == null)
            {
                BuildCalendarUI();
            }
        }

        private void BuildCalendarUI()
        {
            int iDaysInMonth = sysCal.GetDaysInMonth(_DisplayStartDate.Year, _DisplayStartDate.Month);
            int iOffsetDays = System.Convert.ToInt32(System.Enum.ToObject(typeof(System.DayOfWeek), _DisplayStartDate.DayOfWeek));
            int iWeekCount = 0;
            WeekOfDaysControls weekRowCtrl = new WeekOfDaysControls();

            // clear the monthview of all child controls, and reset the namescope to remove all the registered names.
            MonthViewGrid.Children.Clear();
            System.Windows.NameScope.SetNameScope(this, new System.Windows.NameScope());

            AddRowsToMonthGrid(iDaysInMonth, iOffsetDays);
            MonthYearLabel.Content = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_DisplayMonth) + " " + _DisplayYear;

            for (int i = 1; i <= iDaysInMonth; i++)
            {
                if ((i != 1) && System.Math.IEEERemainder((i + iOffsetDays - 1), 7) == 0)
                {
                    // -- add existing weekrowcontrol to the monthgrid
                    Grid.SetRow(weekRowCtrl, iWeekCount);
                    MonthViewGrid.Children.Add(weekRowCtrl);
                    // -- make a new weekrowcontrol
                    weekRowCtrl = new WeekOfDaysControls();
                    iWeekCount += 1;
                }

                // -- load each weekrow with a DayBoxControl whose label is set to day number
                DayBoxControl dayBox = new DayBoxControl
                {
                    Name = "DayBox" + i
                };
           
                var thisDay = new DateTime(_DisplayYear, _DisplayMonth, i);

                dayBox.BindingDate = thisDay;

                dayBox.Tag = i;
                dayBox.MouseDoubleClick += DayBox_DoubleClick;
                dayBox.PreviewDragEnter += DayBox_PreviewEnter;
                dayBox.PreviewDragLeave += DayBox_PreviewLeave;
                dayBox.OnSelectionClick += DayBox_SelectionClick;
                dayBox.OnAppointmentClick += DayBox_AppointmentClick;

                // rest the namescope of the daybox in case user drags appointment from this day to another day, then back again
                System.Windows.NameScope.SetNameScope(dayBox, new System.Windows.NameScope());
                this.RegisterName("DayBox" + i.ToString(), dayBox);

                // -- resets the list of control-names registered with this monthview (to avoid duplicates later)
                System.Windows.NameScope.SetNameScope(dayBox, new System.Windows.NameScope());
                this.RegisterName("DayBox" + i.ToString(), dayBox);

                // -- customize daybox for today:
                if ((new DateTime(_DisplayYear, _DisplayMonth, i)) == DateTime.Today)
                {
                    dayBox.DayLabelRowBorder.Background = _todayBackBrush;
                }

                if (_monthAppointments != null)
                {
                    int iday = i;
                    List<Appointment> aptInDay = _monthAppointments.FindAll(new System.Predicate<Appointment>(apt => ((DateTime)apt.StartTime).Day == iday));
                    if (aptInDay != null && aptInDay.Any())
                    {
                        dayBox.Appointments = new System.Collections.ObjectModel.ObservableCollection<Appointment>(aptInDay);
                    }
                    else
                    {
                        dayBox.Appointments = new System.Collections.ObjectModel.ObservableCollection<Appointment>();
                    }
                }

                Grid.SetColumn(dayBox, (i - (iWeekCount * 7)) + iOffsetDays);
                weekRowCtrl.WeekRowGrid.Children.Add(dayBox);
            }
            Grid.SetRow(weekRowCtrl, iWeekCount);
            MonthViewGrid.Children.Add(weekRowCtrl);
        }



        private void AddRowsToMonthGrid(int DaysInMonth, int OffSetDays)
        {
            MonthViewGrid.RowDefinitions.Clear();
            System.Windows.GridLength rowHeight = new System.Windows.GridLength(60, System.Windows.GridUnitType.Star);

            int EndOffSetDays = 7 - (System.Convert.ToInt32(System.Enum.ToObject(typeof(System.DayOfWeek), _DisplayStartDate.AddDays(DaysInMonth - 1).DayOfWeek)) + 1);

            for (int i = 1; i <= System.Convert.ToInt32((DaysInMonth + OffSetDays + EndOffSetDays) / (double)7); i++)
            {
                var rowDef = new RowDefinition
                {
                    Height = rowHeight
                };
                MonthViewGrid.RowDefinitions.Add(rowDef);
            }
        }

        private void UpdateMonth(int MonthsToAdd)
        {
            this.DisplayStartDate = _DisplayStartDate.AddMonths(MonthsToAdd);

            MonthChangedEventArgs ev = new MonthChangedEventArgs
            {
                OldDisplayStartDate = _DisplayStartDate,
                NewDisplayStartDate = _DisplayStartDate,
            };

            DisplayMonthChanged?.Invoke(ev);
        }


        private void MonthGoPrev_MouseLeftButtonUp(System.Object sender, MouseButtonEventArgs e)
        {
            UpdateMonth(-1);
        }

        private void MonthGoNext_MouseLeftButtonUp(System.Object sender, MouseButtonEventArgs e)
        {
            UpdateMonth(1);
        }

        private void Appointment_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Source.GetType() == typeof(DayBoxAppointmentControl))
            {
                if (((DayBoxAppointmentControl)e.Source).Tag != null)
                {
                    // -- You could put your own call to your appointment-displaying code or whatever here..
                    AppointmentDblClicked?.Invoke(System.Convert.ToInt32(((DayBoxAppointmentControl)e.Source).Tag));
                }

                e.Handled = true;
            }
        }

        private void DayBox_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // -- call to FindVisualAncestor to make sure they didn't click on existing appointment (in which case,
            // that appointment window is already opened by handler Appointment_DoubleClick)
            if (e.Source.GetType() == typeof(DayBoxControl) && (e.OriginalSource as Visual).FindVisualAncestor(typeof(DayBoxAppointmentControl)) == null)
            {
                NewAppointmentEventArgs ev = new NewAppointmentEventArgs();
                if (((DayBoxControl)e.Source).Tag != null)
                {
                    ev.StartDate = new DateTime(_DisplayYear, _DisplayMonth, System.Convert.ToInt32(((DayBoxControl)e.Source).Tag), 10, 0, 0);
                    ev.EndDate = ((DateTime)ev.StartDate).AddHours(2);
                }
                DayBoxDoubleClicked?.Invoke(ev);
                e.Handled = true;
            }
        }

        private void DayBox_PreviewEnter(object sender, System.Windows.DragEventArgs e)
        {
            if (sender.GetType() == typeof(DayBoxControl) && e.Data.GetFormats().Contains(typeof(Appointment).FullName))
            {
                //((DayBoxControl)sender).DayAppointmentsStack.Background = _targetBackBrush;
                e.Handled = true;
            }
        }

        private void DayBox_PreviewLeave(System.Object sender, System.Windows.DragEventArgs e)
        {
            if (sender.GetType() == typeof(DayBoxControl))
            {
                RestoreDayBoxBackground(sender as DayBoxControl);
            }
        }

        private void DayBox_SelectionClick(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(DayBoxControl))
            {
                foreach (var weekRowCtrl in MonthViewGrid.Children)
                {
                    if (weekRowCtrl is WeekOfDaysControls)
                    {
                        foreach (var dayBox in (weekRowCtrl as WeekOfDaysControls).WeekRowGrid.Children)
                        {
                            if (dayBox is DayBoxControl)
                            {
                                if ((dayBox as DayBoxControl) != (sender as DayBoxControl) && (dayBox as DayBoxControl).IsSelected)
                                {
                                    (dayBox as DayBoxControl).IsSelected = false;
                                }
                            }
                        }
                    }
                }

            }
        }

        private void DayBox_AppointmentClick(object sender, ClickAppointmentRoutedEventArgs e)
        {
            if(popup.IsOpen)
            {
                popup.IsOpen = false;
                popup.StaysOpen = false;
            }
            popup.PlacementTarget =(UIElement) sender;
            popup.IsOpen = true;
            popup.StaysOpen = true;
        }

        private void MonthViewGrid_PreviewDrop(object sender, System.Windows.DragEventArgs e)
        {
            Appointment Apt = e.Data.GetData(typeof(Appointment).FullName, false) as Appointment;
            if (Apt != null)
            {
                DayBoxControl DayBoxOld = MonthViewGrid.FindName("DayBox" + Apt.StartTime.Value.Day) as DayBoxControl;
                // only allow drag/drop (move) for appointments already in the current displayed month
                if (DayBoxOld != null)
                {
                    DayBoxControl DayBoxNew = (e.OriginalSource as Visual).FindVisualAncestor(typeof(DayBoxControl)) as DayBoxControl;
                    // only allow drag/drop (move) to days in the current displayed month
                    if (DayBoxNew != null)
                    {
                        DayBoxOld.RemoveAppointment(Apt);
                        // change the start-date and end-date of the appointment to be the date represented by DayBoxNew. *Note that 
                        // the calendar doesn't (yet) support display of multi-day apts, but I'm using an offset since eventually it will.
                        int MoveDays = (int)DayBoxNew.Tag - Apt.StartTime.Value.Day;
                        Apt.StartTime = Apt.StartTime.Value.AddDays(MoveDays);
                        Apt.EndTime = Apt.EndTime.Value.AddDays(MoveDays);

                        // Raise the AppointmentMoved event, which your code will need to handle. Change the args to suit your taste ;-)
                        AppointmentMoved?.Invoke(Apt.AppointmentId, (int)DayBoxOld.Tag, Apt.StartTime.Value.Day);
                        DayBoxNew.AddAppointment(Apt);
                        
                        RestoreDayBoxBackground(DayBoxNew);

                        e.Handled = true;
                    }
                }
            }
        }


        private void RestoreDayBoxBackground(DayBoxControl DayBox)
        {
            if (DayBox.Tag == (object)DateTime.Today.Day)
            {
                //DayBox.DayAppointmentsStack.Background = _todayBackBrush;
            }
            else
            {
                //DayBox.DayAppointmentsStack.Background = _dayBackBrush;
            }
        }


    }

    public class MonthChangedEventArgs
    {
        public DateTime OldDisplayStartDate;
        public DateTime NewDisplayStartDate;
    }

    public class NewAppointmentEventArgs
    {
        public DateTime? StartDate;
        public DateTime? EndDate;
        public int? CandidateId;
        public int? RequirementId;
    }

}
