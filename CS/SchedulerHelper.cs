using System;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;

namespace SchedulerMVVMScenarioWpf {
    public class SchedulerHelper {
        public static readonly DependencyProperty AppointmentsModifiedCommandProperty =
            DependencyProperty.RegisterAttached("AppointmentsModifiedCommand",
            typeof(ICommand), typeof(SchedulerHelper),
            new PropertyMetadata(null, new PropertyChangedCallback(AppointmentsModifiedCommandPropertyChanged)));

        public static readonly DependencyProperty FetchAppointmentsCommandProperty =
            DependencyProperty.RegisterAttached("FetchAppointmentsCommand",
            typeof(ICommand), typeof(SchedulerHelper),
            new PropertyMetadata(null, new PropertyChangedCallback(FetchAppointmentsCommandPropertyChanged)));

        private static ICommand commandAppointmentsModified = null;
        private static ICommand commandFetchAppointments = null;

        private static void AppointmentsModifiedCommandPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            SchedulerControl schedulerControl = obj as SchedulerControl;

            if (schedulerControl != null) {
                schedulerControl.StorageChanged += schedulerControl_StorageChanged;
                commandAppointmentsModified = (ICommand)e.NewValue;
            }
        }

        private static void FetchAppointmentsCommandPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            SchedulerControl schedulerControl = obj as SchedulerControl;

            if (schedulerControl != null) {
                schedulerControl.StorageChanged += schedulerControl_StorageChanged;
                commandFetchAppointments = (ICommand)e.NewValue;
            }
        }

        private static void schedulerControl_StorageChanged(object sender, EventArgs e) {
            SchedulerControl schedulerControl = sender as SchedulerControl;
            SchedulerStorage schedulerStorage = schedulerControl.Storage as SchedulerStorage;

            if (schedulerStorage != null) {
                schedulerStorage.AppointmentsInserted += AppointmentsModifiedAction;
                schedulerStorage.AppointmentsChanged += AppointmentsModifiedAction;
                schedulerStorage.AppointmentsDeleted += AppointmentsModifiedAction;
                schedulerStorage.FetchAppointments += FetchAppointmentsAction;
            }
        }

        private static void AppointmentsModifiedAction(object sender, PersistentObjectsEventArgs e) {
            if (commandAppointmentsModified != null)
                commandAppointmentsModified.Execute(null);
        }

        private static void FetchAppointmentsAction(object sender, FetchAppointmentsEventArgs e) {
            if (commandFetchAppointments != null)
                commandFetchAppointments.Execute(new object[] { 
                    ((DevExpress.Xpf.Scheduler.Native.SchedulerDataStorage)sender).Appointments.DataSource, e.Interval 
                });
        }

        public static void SetAppointmentsModifiedCommand(UIElement element, ICommand value) {
            element.SetValue(AppointmentsModifiedCommandProperty, value);
        }

        public static ICommand GetAppointmentsModifiedCommand(UIElement element) {
            return (ICommand)element.GetValue(AppointmentsModifiedCommandProperty);
        }

        public static void SetFetchAppointmentsCommand(UIElement element, ICommand value) {
            element.SetValue(FetchAppointmentsCommandProperty, value);
        }

        public static ICommand GetFetchAppointmentsCommand(UIElement element) {
            return (ICommand)element.GetValue(FetchAppointmentsCommandProperty);
        }
    }
}