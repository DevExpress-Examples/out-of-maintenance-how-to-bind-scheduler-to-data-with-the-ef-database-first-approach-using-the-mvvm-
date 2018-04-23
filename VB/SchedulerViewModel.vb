Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Data.Objects
Imports System.Windows.Forms
Imports System.Windows.Input
Imports DevExpress.Xpf.Core.Commands
Imports DevExpress.XtraScheduler

Namespace SchedulerMVVMScenarioWpf
	Public Class SchedulerViewModel
		Inherits ObservableObject
		Private lastFetchedInterval As TimeInterval
		Private carsXtraSchedulingEntities As CarsXtraSchedulingEntities

		Private appointments_Renamed As BindingSource
		Private resources_Renamed As ObjectSet(Of Car)

		Public Property Appointments() As BindingSource
			Get
				Return appointments_Renamed
			End Get
			Private Set(ByVal value As BindingSource)
				appointments_Renamed = value
				OnPropertyChanged("Appointments")
			End Set
		End Property

		Public Property Resources() As ObjectSet(Of Car)
			Get
				Return resources_Renamed
			End Get
			Private Set(ByVal value As ObjectSet(Of Car))
				resources_Renamed = value
				OnPropertyChanged("Resources")
			End Set
		End Property

		Private privateAppointmentsModifiedCommand As ICommand
		Public Property AppointmentsModifiedCommand() As ICommand
			Get
				Return privateAppointmentsModifiedCommand
			End Get
			Private Set(ByVal value As ICommand)
				privateAppointmentsModifiedCommand = value
			End Set
		End Property
		Private privateFetchAppointmentsCommand As ICommand
		Public Property FetchAppointmentsCommand() As ICommand
			Get
				Return privateFetchAppointmentsCommand
			End Get
			Private Set(ByVal value As ICommand)
				privateFetchAppointmentsCommand = value
			End Set
		End Property

		Public Sub New()
			lastFetchedInterval = New TimeInterval()
			carsXtraSchedulingEntities = New CarsXtraSchedulingEntities()
			Resources = carsXtraSchedulingEntities.Cars
			Appointments = New BindingSource()

			AppointmentsModifiedCommand = New DelegateCommand(Of Object)(AddressOf AppointmentsModifiedExecute)
			FetchAppointmentsCommand = New DelegateCommand(Of Object())(AddressOf FetchAppointmentsExecute)
		End Sub

		Private Sub AppointmentsModifiedExecute(ByVal parameter As Object)
			carsXtraSchedulingEntities.SaveChanges()
		End Sub

		Private Sub FetchAppointmentsExecute(ByVal paramseters() As Object)
			Dim bindingSource As BindingSource = TryCast(paramseters(0), BindingSource)
			Dim currentInterval As TimeInterval = TryCast(paramseters(1), TimeInterval)

			If bindingSource Is Nothing OrElse lastFetchedInterval.Contains(currentInterval) Then
				Return
			End If

			Dim margin As TimeSpan = TimeSpan.FromDays(0) ' TimeSpan.FromDays(1)
			lastFetchedInterval = New TimeInterval(currentInterval.Start - margin, currentInterval.End + margin)

			Dim entities = _
				From entity In carsXtraSchedulingEntities.CarSchedulings _
				Where entity.EventType = 1 OrElse (entity.StartTime < lastFetchedInterval.End AndAlso entity.EndTime > lastFetchedInterval.Start) _
				Select entity

			bindingSource.DataSource = entities
		End Sub
	End Class
End Namespace