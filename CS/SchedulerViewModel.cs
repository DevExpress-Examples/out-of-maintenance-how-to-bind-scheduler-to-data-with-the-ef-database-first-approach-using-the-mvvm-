using System;
using System.Linq;
using System.Data.Objects;
using System.Windows.Forms;
using System.Windows.Input;
using DevExpress.Xpf.Core.Commands;
using DevExpress.XtraScheduler;

namespace SchedulerMVVMScenarioWpf {
    public class SchedulerViewModel : ObservableObject {
        private TimeInterval lastFetchedInterval;
        private CarsXtraSchedulingEntities carsXtraSchedulingEntities;

        private BindingSource appointments;
        private ObjectSet<Car> resources;

        public BindingSource Appointments {
            get { return appointments; }
            private set {
                appointments = value;
                OnPropertyChanged("Appointments");
            }
        }

        public ObjectSet<Car> Resources {
            get { return resources; }
            private set {
                resources = value;
                OnPropertyChanged("Resources");
            }
        }

        public ICommand AppointmentsModifiedCommand { get; private set; }
        public ICommand FetchAppointmentsCommand { get; private set; }

        public SchedulerViewModel() {
            lastFetchedInterval = new TimeInterval();
            carsXtraSchedulingEntities = new CarsXtraSchedulingEntities();
            Resources = carsXtraSchedulingEntities.Cars;
            Appointments = new BindingSource();
           
            AppointmentsModifiedCommand = new DelegateCommand<object>(AppointmentsModifiedExecute);
            FetchAppointmentsCommand = new DelegateCommand<object[]>(FetchAppointmentsExecute);
        }

        private void AppointmentsModifiedExecute(object parameter) {
            carsXtraSchedulingEntities.SaveChanges();
        }

        private void FetchAppointmentsExecute(object[] paramseters) {
            BindingSource bindingSource = paramseters[0] as BindingSource;
            TimeInterval currentInterval = paramseters[1] as TimeInterval;

            if (bindingSource == null || lastFetchedInterval.Contains(currentInterval))
                return;

            TimeSpan margin = TimeSpan.FromDays(0); // TimeSpan.FromDays(1) 
            lastFetchedInterval = new TimeInterval(currentInterval.Start - margin, currentInterval.End + margin);

            var entities = from entity in carsXtraSchedulingEntities.CarSchedulings
                           where entity.EventType == 1 || (entity.StartTime < lastFetchedInterval.End && entity.EndTime > lastFetchedInterval.Start)
                           select entity;

            bindingSource.DataSource = entities;
        }
    }
}