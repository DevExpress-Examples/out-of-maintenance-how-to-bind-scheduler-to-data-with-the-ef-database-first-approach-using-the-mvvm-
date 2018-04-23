Imports System
Imports System.Linq
Imports System.Data.Objects
Imports System.Windows.Forms
Imports System.Windows.Input

Imports DevExpress.XtraScheduler
Imports System.Data.Entity
Imports System.ComponentModel
Imports System.Collections.Generic
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Scheduler

Namespace SchedulerMVVMScenarioWpf
    Public Class SchedulerViewModel
        Implements INotifyPropertyChanged

        Private lastFetchedInterval As TimeInterval
        Private carsXtraSchedulingEntities As CarsXtraSchedulingEntities


        Private appointments_Renamed As BindingList(Of CarScheduling)

        Private resources_Renamed As BindingList(Of Car)

        Public Sub New()
            lastFetchedInterval = New TimeInterval()
            carsXtraSchedulingEntities = New CarsXtraSchedulingEntities()
            carsXtraSchedulingEntities.Cars.Load()
            Resources = carsXtraSchedulingEntities.Cars.Local.ToBindingList()

            AppointmentsModifiedCommand = New DelegateCommand(Of DevExpress.XtraScheduler.PersistentObjectsEventArgs)(AddressOf AppointmentsModifiedExecute)
            FetchAppointmentsCommand = New DelegateCommand(Of DevExpress.XtraScheduler.FetchAppointmentsEventArgs)(AddressOf FetchAppointmentsExecute)
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub OnPropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

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
        Public Property Appointments() As BindingList(Of CarScheduling)
            Get
                Return appointments_Renamed
            End Get
            Private Set(ByVal value As BindingList(Of CarScheduling))
                appointments_Renamed = value
                OnPropertyChanged("Appointments")
            End Set
        End Property
        Public Property Resources() As BindingList(Of Car)
            Get
                Return resources_Renamed
            End Get
            Private Set(ByVal value As BindingList(Of Car))
                resources_Renamed = value
                OnPropertyChanged("Resources")
            End Set
        End Property

        Private Sub AppointmentsModifiedExecute(ByVal e As DevExpress.XtraScheduler.PersistentObjectsEventArgs)
            carsXtraSchedulingEntities.SaveChanges()
        End Sub

        Private Sub FetchAppointmentsExecute(ByVal e As DevExpress.XtraScheduler.FetchAppointmentsEventArgs)
            Dim currentInterval As TimeInterval = TryCast(e.Interval, TimeInterval)

            If lastFetchedInterval.Contains(currentInterval) Then
                Return
            End If

            lastFetchedInterval = currentInterval
            Dim entities = From entity In carsXtraSchedulingEntities.CarSchedulings _
                           Where entity.EventType > 0 OrElse (entity.StartTime < lastFetchedInterval.End AndAlso entity.EndTime > lastFetchedInterval.Start) _
                           Select entity
            entities.Load()
            Appointments = carsXtraSchedulingEntities.CarSchedulings.Local.ToBindingList()
        End Sub


    End Class
End Namespace