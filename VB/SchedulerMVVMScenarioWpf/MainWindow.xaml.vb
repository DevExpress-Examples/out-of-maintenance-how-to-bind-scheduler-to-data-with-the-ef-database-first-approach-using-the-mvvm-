Imports System
Imports System.Windows

Namespace SchedulerMVVMScenarioWpf
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
            schedulerControl1.Start = Date.Now
        End Sub
    End Class
End Namespace