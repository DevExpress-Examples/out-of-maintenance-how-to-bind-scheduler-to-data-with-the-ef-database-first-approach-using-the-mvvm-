using System.Windows;

namespace SchedulerMVVMScenarioWpf {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            schedulerControl1.Start = new System.DateTime(2008, 7, 1);
        }
    }
}