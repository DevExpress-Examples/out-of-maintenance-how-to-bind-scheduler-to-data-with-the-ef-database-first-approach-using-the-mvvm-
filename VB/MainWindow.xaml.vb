Imports Microsoft.VisualBasic
Imports System.Windows

Namespace SchedulerMVVMScenarioWpf
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()
			schedulerControl1.Start = New System.DateTime(2008, 7, 1)
		End Sub
	End Class
End Namespace