using System;
using System.Windows;

namespace SchedulerMVVMScenarioWpf {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            schedulerControl1.Start = DateTime.Now;
        }
    }
}