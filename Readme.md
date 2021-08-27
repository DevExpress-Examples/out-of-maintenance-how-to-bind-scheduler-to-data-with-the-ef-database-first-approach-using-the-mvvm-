<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128656620/15.2.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4670)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# How to bind Scheduler to data with the EF Database First approach using the MVVM pattern


<p>This example illustrates how to implement a scheduling application implementing the MVVM pattern (see <a href="http://en.wikipedia.org/wiki/Model_View_ViewModel">Model View ViewModel</a>). In this example, a SchedulerControl is bound to data using <a href="http://www.entityframeworktutorial.net/database-first-with-entity-framework.aspx">Entity Framework Database First</a> approach.</p>


<h3>Description</h3>

<p>The Model is represented by the&nbsp;<strong>Car&nbsp;</strong>and&nbsp;<strong>CarScheduling&nbsp;</strong>classes generated using an&nbsp;<a href="http://en.wikipedia.org/wiki/Entity_Framework">Entity Framework</a>&nbsp;layer. The&nbsp;<strong>SchedulerViewModel</strong>&nbsp;class represents a view model.&nbsp;<br><br>In this example, appointments are retrieved by portions using the&nbsp;<a href="https://documentation.devexpress.com/#WPF/DevExpressXpfSchedulerSchedulerStorage_FetchAppointmentstopic">SchedulerStorage.FetchAppointments</a>&nbsp;event. To save modifications to the database, the&nbsp;<a href="https://documentation.devexpress.com/#WPF/DevExpressXpfSchedulerSchedulerStorage_AppointmentsInsertedtopic">SchedulerStorage.AppointmentsInserted</a>,&nbsp;<a href="https://documentation.devexpress.com/#WPF/DevExpressXpfSchedulerSchedulerStorage_AppointmentsDeletedtopic">SchedulerStorage.AppointmentsDeleted</a>&nbsp;and&nbsp;<a href="https://documentation.devexpress.com/#WPF/DevExpressXpfSchedulerSchedulerStorage_AppointmentsChangedtopic">SchedulerStorage.AppointmentsChanged</a>&nbsp;events are handled. These events are bound to the&nbsp;&nbsp;<strong>AppointmentsModifiedCommand</strong>&nbsp;and&nbsp;<strong>FetchAppointmentsCommand&nbsp;</strong>commands in the ViewModel using the&nbsp;<a href="https://documentation.devexpress.com/#WPF/CustomDocument17369/Example">EventToCommand</a>&nbsp;DevExpress&nbsp;<a href="https://documentation.devexpress.com/#WPF/CustomDocument15112">MVVM Framework</a>&nbsp;behavior.</p>

<br/>


