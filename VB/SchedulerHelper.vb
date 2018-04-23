Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.XtraScheduler

Namespace SchedulerMVVMScenarioWpf
	Public Class SchedulerHelper
		Public Shared ReadOnly AppointmentsModifiedCommandProperty As DependencyProperty = DependencyProperty.RegisterAttached("AppointmentsModifiedCommand", GetType(ICommand), GetType(SchedulerHelper), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf AppointmentsModifiedCommandPropertyChanged)))

		Public Shared ReadOnly FetchAppointmentsCommandProperty As DependencyProperty = DependencyProperty.RegisterAttached("FetchAppointmentsCommand", GetType(ICommand), GetType(SchedulerHelper), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf FetchAppointmentsCommandPropertyChanged)))

		Private Shared commandAppointmentsModified As ICommand = Nothing
		Private Shared commandFetchAppointments As ICommand = Nothing

		Private Shared Sub AppointmentsModifiedCommandPropertyChanged(ByVal obj As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
			Dim schedulerControl As SchedulerControl = TryCast(obj, SchedulerControl)

			If schedulerControl IsNot Nothing Then
				AddHandler schedulerControl.StorageChanged, AddressOf schedulerControl_StorageChanged
				commandAppointmentsModified = CType(e.NewValue, ICommand)
			End If
		End Sub

		Private Shared Sub FetchAppointmentsCommandPropertyChanged(ByVal obj As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
			Dim schedulerControl As SchedulerControl = TryCast(obj, SchedulerControl)

			If schedulerControl IsNot Nothing Then
				AddHandler schedulerControl.StorageChanged, AddressOf schedulerControl_StorageChanged
				commandFetchAppointments = CType(e.NewValue, ICommand)
			End If
		End Sub

		Private Shared Sub schedulerControl_StorageChanged(ByVal sender As Object, ByVal e As EventArgs)
			Dim schedulerControl As SchedulerControl = TryCast(sender, SchedulerControl)
			Dim schedulerStorage As SchedulerStorage = TryCast(schedulerControl.Storage, SchedulerStorage)

			If schedulerStorage IsNot Nothing Then
				AddHandler schedulerStorage.AppointmentsInserted, AddressOf AppointmentsModifiedAction
				AddHandler schedulerStorage.AppointmentsChanged, AddressOf AppointmentsModifiedAction
				AddHandler schedulerStorage.AppointmentsDeleted, AddressOf AppointmentsModifiedAction
				AddHandler schedulerStorage.FetchAppointments, AddressOf FetchAppointmentsAction
			End If
		End Sub

		Private Shared Sub AppointmentsModifiedAction(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
			If commandAppointmentsModified IsNot Nothing Then
				commandAppointmentsModified.Execute(Nothing)
			End If
		End Sub

		Private Shared Sub FetchAppointmentsAction(ByVal sender As Object, ByVal e As FetchAppointmentsEventArgs)
			If commandFetchAppointments IsNot Nothing Then
				commandFetchAppointments.Execute(New Object() { (CType(sender, DevExpress.Xpf.Scheduler.Native.SchedulerDataStorage)).Appointments.DataSource, e.Interval })
			End If
		End Sub

		Public Shared Sub SetAppointmentsModifiedCommand(ByVal element As UIElement, ByVal value As ICommand)
			element.SetValue(AppointmentsModifiedCommandProperty, value)
		End Sub

		Public Shared Function GetAppointmentsModifiedCommand(ByVal element As UIElement) As ICommand
			Return CType(element.GetValue(AppointmentsModifiedCommandProperty), ICommand)
		End Function

		Public Shared Sub SetFetchAppointmentsCommand(ByVal element As UIElement, ByVal value As ICommand)
			element.SetValue(FetchAppointmentsCommandProperty, value)
		End Sub

		Public Shared Function GetFetchAppointmentsCommand(ByVal element As UIElement) As ICommand
			Return CType(element.GetValue(FetchAppointmentsCommandProperty), ICommand)
		End Function
	End Class
End Namespace