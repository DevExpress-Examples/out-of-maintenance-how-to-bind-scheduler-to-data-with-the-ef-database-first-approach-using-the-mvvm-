# How to bind Scheduler to data with the EF Database First approach using the MVVM pattern


<p>This example illustrates how to implement a scheduling application implementing the MVVM pattern (see <a href="http://en.wikipedia.org/wiki/Model_View_ViewModel">Model View ViewModel</a>). In this example, a SchedulerControl is bound to data using <a href="http://www.entityframeworktutorial.net/database-first-with-entity-framework.aspx">Entity Framework Database First</a> approach.</p>


<h3>Description</h3>

<p>Model is represented by the <strong>Car </strong>and <strong>CarScheduling </strong>class generated via an <a href="http://en.wikipedia.org/wiki/Entity_Framework">Entity Framework</a> layer. The <strong>SchedulerViewModel</strong> class represents a view model. It exposes model collections (for appointments and resources) to the view that is not defined separately integrated into the <strong>MainWindow.xaml</strong> markup to keep things simple. In addition, we define two commands in the view model: <strong>AppointmentsModifiedCommand</strong> and <strong>FetchAppointmentsCommand</strong>. We cannot use the <a href="http://adammills.wordpress.com/2011/02/14/eventtocommand-action-mvvm-glue/">EventToCommand</a> from the <a href="http://mvvmlight.codeplex.com/">MVVM Light Toolkit</a> to transform the corresponding SchedulerStorage events to these commands because these events are not routed (they are defined as regular CLR events). Thus, we have created a special <strong>SchedulerHelper </strong>class with the corresponding attached properties to be able to map events to commands in XAML in the following manner:</p>
<pre class="cr-code">[XAML]<code>        &lt;dxsch:SchedulerControl Name="schedulerControl1" ActiveViewType="Timeline" GroupType="Resource"
                                local:SchedulerHelper.AppointmentsModifiedCommand="{Binding AppointmentsModifiedCommand}"
                                local:SchedulerHelper.FetchAppointmentsCommand="{Binding FetchAppointmentsCommand}"&gt;
        ...</code></pre>
<p><strong>Note:</strong> This approach is illustrated in the <a data-ticket="Q419179">Use EventToCommand with Appointment events</a> ticket. Alternatively, you can implement the approach from the <a data-ticket="Q430988">Scheduler - Event handling - EventTrigger / EventToCommand / MVVM</a> ticket, i.e., use a custom-made <strong>CLREventTrigger</strong> class.</p>

<br/>


