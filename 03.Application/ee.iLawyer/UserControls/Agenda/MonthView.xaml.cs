
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

                BuildCalendarUI();
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

            BuildDayBoxes();
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

        //private void BuildCalendarUI()
        //{
        //    int daysInCurrentMonth = sysCal.GetDaysInMonth(_DisplayStartDate.Year, _DisplayStartDate.Month);

        //    int preYear = _DisplayStartDate.Month == 1 ? _DisplayStartDate.Year - 1 : _DisplayStartDate.Year;
        //    int preMonth = _DisplayStartDate.Month == 1 ? 12 : _DisplayStartDate.Month - 1;
        //    int daysInPreMonth = sysCal.GetDaysInMonth(preYear, preMonth);

        //    int offsetDays = System.Convert.ToInt32(System.Enum.ToObject(typeof(System.DayOfWeek), _DisplayStartDate.DayOfWeek)) - 1;

        //    // clear the monthview of all child controls, and reset the namescope to remove all the registered names.
        //    MonthViewGrid.Children.Clear();
        //    System.Windows.NameScope.SetNameScope(this, new System.Windows.NameScope());

        //    MonthYearLabel.Content = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_DisplayMonth) + " " + _DisplayYear;


        //    for (int row = 0; row < 6; row++)
        //    {
        //        WeekOfDaysControls weekRowCtrl = new WeekOfDaysControls();
        //        for (int column = 1; column <= 7; column++)
        //        {       // -- load each weekrow with a DayBoxControl whose label is set to day number
        //            DayBoxControl dayBox = new DayBoxControl();
        //            DateTime currentDay;
        //            if (row == 0 && column <= offsetDays)//previous month
        //            {
        //                dayBox.Name = $"DayBox{preYear}{preMonth}{daysInPreMonth - offsetDays + column}";
        //                currentDay = new DateTime(preYear, preMonth, (daysInPreMonth - offsetDays + column));
        //            }
        //            else
        //            {
        //                var currentMonthIndex = row * 7 - offsetDays + column;
        //                if (currentMonthIndex > daysInCurrentMonth)
        //                {
        //                    int nextYear = _DisplayStartDate.Month == 12 ? _DisplayStartDate.Year + 1 : _DisplayStartDate.Year;
        //                    int nextMonth = _DisplayStartDate.Month == 12 ? 1 : _DisplayStartDate.Month + 1;
        //                    int daysInNextMonth = sysCal.GetDaysInMonth(nextYear, nextMonth);

        //                    dayBox.Name = $"DayBox{nextYear}{nextMonth}{currentMonthIndex - daysInCurrentMonth}";
        //                    currentDay = new DateTime(nextYear, nextMonth, (currentMonthIndex - daysInCurrentMonth));
        //                }
        //                else
        //                {
        //                    dayBox.Name = $"DayBox{_DisplayStartDate.Year}{_DisplayStartDate.Month}{currentMonthIndex}";
        //                    currentDay = new DateTime(_DisplayYear, _DisplayMonth, (currentMonthIndex));
        //                }
        //            }


        //            dayBox.BindingDate = currentDay;
        //            dayBox.MouseDoubleClick += DayBox_DoubleClick;
        //            dayBox.PreviewDragEnter += DayBox_PreviewEnter;
        //            dayBox.PreviewDragLeave += DayBox_PreviewLeave;
        //            dayBox.OnSelectionClick += DayBox_SelectionClick;
        //            dayBox.OnAppointmentClick += DayBox_AppointmentClick;

        //            // rest the namescope of the daybox in case user drags appointment from this day to another day, then back again
        //            System.Windows.NameScope.SetNameScope(dayBox, new System.Windows.NameScope());
        //            this.RegisterName(dayBox.Name, dayBox);


        //            // -- customize daybox for today:
        //            if (currentDay == DateTime.Today)
        //            {
        //                dayBox.DayLabelRowBorder.Background = _todayBackBrush;
        //            }

        //            if (_monthAppointments != null)
        //            {
        //                List<Appointment> aptInDay = _monthAppointments.FindAll(new System.Predicate<Appointment>(apt => ((DateTime)apt.StartTime) == currentDay));
        //                if (aptInDay != null && aptInDay.Any())
        //                {
        //                    dayBox.Appointments = new System.Collections.ObjectModel.ObservableCollection<Appointment>(aptInDay);
        //                }
        //                else
        //                {
        //                    dayBox.Appointments = new System.Collections.ObjectModel.ObservableCollection<Appointment>();
        //                }
        //            }


        //            Grid.SetColumn(dayBox, column);
        //            weekRowCtrl.WeekRowGrid.Children.Add(dayBox);
        //        }

        //        Grid.SetRow(weekRowCtrl, row);
        //        MonthViewGrid.Children.Add(weekRowCtrl);
        //    }



        //}




        private void BuildCalendarUI()
        {
            int daysInCurrentMonth = sysCal.GetDaysInMonth(_DisplayStartDate.Year, _DisplayStartDate.Month);

            int preYear = _DisplayStartDate.Month == 1 ? _DisplayStartDate.Year - 1 : _DisplayStartDate.Year;
            int preMonth = _DisplayStartDate.Month == 1 ? 12 : _DisplayStartDate.Month - 1;
            int daysInPreMonth = sysCal.GetDaysInMonth(preYear, preMonth);

            int offsetDays = System.Convert.ToInt32(System.Enum.ToObject(typeof(System.DayOfWeek), _DisplayStartDate.DayOfWeek)) ;
            if (offsetDays == 0) offsetDays = 7;
            offsetDays = offsetDays - 1;

            MonthYearLabel.Content = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_DisplayMonth) + " " + _DisplayYear;


            for (int row = 0; row < 6; row++)
            {
                WeekOfDaysControls weekRowCtrl = new WeekOfDaysControls();
                for (int column = 1; column <= 7; column++)
                {
                    var controlName = $"DayBox{row * 7 + column}";
                    DayBoxControl dayBox = Utilities.FindName<DayBoxControl>(MonthViewGrid, controlName);
                    if (dayBox == null) continue;

                    DateTime currentDay;
                    if (row == 0 && column <= offsetDays)//previous month
                    {
                        currentDay = new DateTime(preYear, preMonth, (daysInPreMonth - offsetDays + column));
                        dayBox.Ashing(true);
                    }
                    else
                    {
                        var currentMonthIndex = row * 7 - offsetDays + column;
                        if (currentMonthIndex > daysInCurrentMonth)
                        {
                            int nextYear = _DisplayStartDate.Month == 12 ? _DisplayStartDate.Year + 1 : _DisplayStartDate.Year;
                            int nextMonth = _DisplayStartDate.Month == 12 ? 1 : _DisplayStartDate.Month + 1;
                            int daysInNextMonth = sysCal.GetDaysInMonth(nextYear, nextMonth);

                            currentDay = new DateTime(nextYear, nextMonth, (currentMonthIndex - daysInCurrentMonth));
                            dayBox.Ashing(true);
                        }
                        else
                        {
                            currentDay = new DateTime(_DisplayYear, _DisplayMonth, (currentMonthIndex));
                            dayBox.Ashing(false);
                        }
                    }
                 
                    if (dayBox != null)
                    {

                        dayBox.BindingDate = currentDay;

                   

                        if (_monthAppointments != null)
                        {
                            List<Appointment> aptInDay = _monthAppointments.FindAll(new System.Predicate<Appointment>(apt => ((DateTime)apt.StartTime) == currentDay));
                            if (aptInDay != null && aptInDay.Any())
                            {
                                dayBox.Appointments = new System.Collections.ObjectModel.ObservableCollection<Appointment>(aptInDay);
                            }
                            else
                            {
                                dayBox.Appointments = new System.Collections.ObjectModel.ObservableCollection<Appointment>();
                            }
                        }
                    }
                }
            }
        }




        private void BuildDayBoxes()
        {

            MonthViewGrid.Children.Clear();
            System.Windows.NameScope.SetNameScope(this, new System.Windows.NameScope());

            MonthYearLabel.Content = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_DisplayMonth) + " " + _DisplayYear;


            for (int row = 0; row < 6; row++)
            {
                WeekOfDaysControls weekRowCtrl = new WeekOfDaysControls();
                for (int column = 1; column <= 7; column++)
                {
                    DayBoxControl dayBox = new DayBoxControl()
                    {
                        Name = $"DayBox{row * 7 + column}",
                    };

                    dayBox.MouseDoubleClick += DayBox_DoubleClick;
                    dayBox.PreviewDragEnter += DayBox_PreviewEnter;
                    dayBox.PreviewDragLeave += DayBox_PreviewLeave;
                    dayBox.OnSelectionClick += DayBox_SelectionClick;
                    dayBox.OnAppointmentClick += DayBox_AppointmentClick;

                    // rest the namescope of the daybox in case user drags appointment from this day to another day, then back again
                    System.Windows.NameScope.SetNameScope(dayBox, new System.Windows.NameScope());
                    this.RegisterName(dayBox.Name, dayBox);

                    Grid.SetColumn(dayBox, column);
                    weekRowCtrl.WeekRowGrid.Children.Add(dayBox);
                }

                Grid.SetRow(weekRowCtrl, row);
                MonthViewGrid.Children.Add(weekRowCtrl);
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
            if (popup.IsOpen)
            {
                popup.IsOpen = false;
                popup.StaysOpen = false;
            }
            popup.PlacementTarget = (UIElement)sender;
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
