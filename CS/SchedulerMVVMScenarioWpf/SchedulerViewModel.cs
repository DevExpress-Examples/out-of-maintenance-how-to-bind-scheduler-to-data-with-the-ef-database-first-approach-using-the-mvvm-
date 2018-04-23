using System;
using System.Linq;
using System.Data.Objects;
using System.Windows.Forms;
using System.Windows.Input;

using DevExpress.XtraScheduler;
using System.Data.Entity;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.Mvvm;
using DevExpress.Xpf.Scheduler;

namespace SchedulerMVVMScenarioWpf {
    public class SchedulerViewModel : INotifyPropertyChanged {
        private TimeInterval lastFetchedInterval;
        private CarsXtraSchedulingEntities carsXtraSchedulingEntities;

        private BindingList<CarScheduling> appointments;
        private BindingList<Car> resources;

        public SchedulerViewModel() {
            lastFetchedInterval = new TimeInterval();
            carsXtraSchedulingEntities = new CarsXtraSchedulingEntities();
            carsXtraSchedulingEntities.Cars.Load();
            Resources = carsXtraSchedulingEntities.Cars.Local.ToBindingList();
           
            AppointmentsModifiedCommand = new DelegateCommand< DevExpress.XtraScheduler.PersistentObjectsEventArgs>(AppointmentsModifiedExecute);
            FetchAppointmentsCommand = new DelegateCommand<DevExpress.XtraScheduler.FetchAppointmentsEventArgs>(FetchAppointmentsExecute);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String propertyName) {
            if(this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand AppointmentsModifiedCommand {
            get;
            private set;
        }
        public ICommand FetchAppointmentsCommand {
            get;
            private set;
        }
        public BindingList<CarScheduling> Appointments {
            get {
                return appointments;
            }
            private set {
                appointments = value;
                OnPropertyChanged("Appointments");
            }
        }
        public BindingList<Car> Resources {
            get {
                return resources;
            }
            private set {
                resources = value;
                OnPropertyChanged("Resources");
            }
        }

        private void AppointmentsModifiedExecute( DevExpress.XtraScheduler.PersistentObjectsEventArgs e) {
            carsXtraSchedulingEntities.SaveChanges();
        }

        private void FetchAppointmentsExecute(DevExpress.XtraScheduler.FetchAppointmentsEventArgs e) {
            TimeInterval currentInterval = e.Interval as TimeInterval;

            if (lastFetchedInterval.Contains(currentInterval))
                return;

            lastFetchedInterval = currentInterval;
            var entities = from entity in carsXtraSchedulingEntities.CarSchedulings
                           where entity.EventType > 0 || (entity.StartTime < lastFetchedInterval.End && entity.EndTime > lastFetchedInterval.Start)
                           select entity;
            entities.Load();
            Appointments = carsXtraSchedulingEntities.CarSchedulings.Local.ToBindingList();
        }

       
    }
}