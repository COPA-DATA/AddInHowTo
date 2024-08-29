The zenon software platform is a software system from COPA-DATA for industrial automation and the energy industry. Machines and equipment are controlled, monitored and optimized. zenon's particular strength is open and reliable communication throughout heterogeneous production facilities. Over 300 native communication protocols support the horizontal and vertical exchange of data. This allows for the continuous implementation of Industrial IoT and the Smart Factory.

zenon’s engineering environment is flexible and can be used in many ways. Complex functions for comprehensive applications such as HMI/SCADA, reporting and IIoT are supplied out of the box to create intuitive and robust applications. Users can thus contribute to the increased flexibility and efficiency of applications using zenon.

To open projects of this repository please install Visual Studio Developer Tools, they available at [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=vs-publisher-1463468.COPA-DATASCADAAdd-InDeveloperToolsforVS) 

This repository contains frequently asked sample of zenon Add-In solutions. All projects of this folder are added to solution [HowTo.sln](HowTo.sln).

# Contents
 1. [AddIn sample library](#addinsamplelibrary) - Subscribe, Communicate and Log
 2. [ClickUpDown](#clickupdown) - React on Screen Element events
 3. [DriverConfigurationSamples](#driverconfigurationsamples) - Ever wondered how to automatically configure drivers?
 4. [DynamicFilterSample](#dynamicfiltersample) - one screen switch function is enough!
 5. [DynPropertyExtensions](#dynpropertyextensions) - never look up Dynamic Property names again.
 6. [ExtensionWpfElementInteraction](#extensionwpfelementinteraction) - inter process communication
 7. [MvvmEditorWizard](#mvvmeditorwizard) - proper visualization architecture
 8. [ObjectCreationWithXmlImport](#objectcreationwithxmlimport) - for creating lots of things
 9. [ThreadingWizardSample](#threadingwizardsample) - never block zenon again with AddIns
 10. [VariableReadWrite](#variablereadwrite) - frequently asked how-to.
 11. [VariableSubscriptionSample](#variablesubscriptionsample) - react non-blocking on a variable change.

# AddInSampleLibrary
<a name="AddInSampleLibrary"></a>
This project demonstrates show to build custom libraries to share common code between zenon Add-Ins. Following common classes are added to this project:

* Subscription\VariableSubscription.cs: Class VariableSubscription is responsible for variable subscription, by creating an online container and handling the bulk changed event. The changed event uses TPL (Task Parallel Library) to ensure that zenon Service Engine gets not blocked during handling a (simulated) long running operation.
* Logging\NLogConfigurator.sln: A reusable class to add logging functionality to Add-Ins. This code is derived from the training sample [NLogSample](#NLogSample)
* Communication: Reusable classes to interact between Add-In Extensions, WPF Elements or external code. This sample is documentated [here](#ExtensionWpfElementInteraction)
* ErrorHandler.cs: Is used to check return values of the zenon API to ensure that the operation that has been called was successfully completed.

## AddInSampleLibraryTests
Contains a Unit Test project, that demonstrates how to unit test classes that is using zenon API. The zenon API is simulated by using [Moq](https://github.com/moq/moq). A getting started guide for [Moq is available here](https://github.com/Moq/moq4/wiki/Quickstart). 

This sample is based on [AddInSampleLibrary](#AddInSampleLibrary).

[back to Top](#contents)

# ClickUpDown
<a name="ClickUpDown"></a>
This project implements a service extension which reacts on events triggered by mouse movements over zenon screen elements. Note that the events are triggered on the screen element collection for all screen elements. The developer of the AddIn has to take care for different reactions on different elements.  

> **Note:** This is contradictory to events in VBA: Those can be linked to the individual screen elements in the zenon property group `VBA macros`. Still, the use of VBA is not recommended!

[back to Top](#contents)

# DriverConfigurationSamples
<a name="DriverConfigurationSamples"></a>
This folder contains Engineering Studio Wizard examples for configuring following drivers:
- 3S_V3
- BACNetNG
- BeckhNG
- BURPVI
- CIFMPI
- DNP3_TG
- IEC850
- Logix32
- OPCUA32
- Phloem

The samples for the driver configuration use the `DriverCommon` project. This implements general handling of driver configuration. The driver configuration itself is done with the concept of DynProperties.

[back to Top](#contents)

# DynamicFilterSample
<a name="DynamicFilterSample"></a>
This Service Engine wizard exemplifies the modification of a screen switch function. The function is immediately executed via the API with the modification. The modification is only temporarely and is not saved in the zenon project. For an easier access to this sample a zenon project backup is included. 

[back to Top](#contents)

# DynPropertyExtensions
<a name="DynamicFilterSample"></a>

>**Note:** This is experimental. Some parts might not work as expected.

It might be cumbersome to find out the correct string identifier and data type for a DynProperty in zenon. To make things easier you can use this set of generated extensions (Use the files in the according folder for your version of zenon). These extensions extend the zenon datatype and allow you to see the available DynProperties in the IDE immediately.

``` csharp
// instead of this, where you have to find out the propertyPath
 var fontName = (string)firstFont.GetDynamicProperty("Name"); 
 // ...extensions allow you to use this approach:
 var fontNameByExtension = firstFont.GetName();         
```

[back to Top](#contents)

# ExtensionWpfElementInteraction
<a name="ExtensionWpfElementInteraction"></a>
This sample demonstrates how to communicate between Add-In Extensions and WPF Elements. Every extension is separated using AppDomains therefore a direct interaction between Add-In Extensions and also WPF Elements is not possible by design. Using Inter Process Communication (IPC) allows to
share data between Add-Ins.

The library called "CommunicationLibrary" contains code that is shared between Add-Ins and WPF elements. Here two interfaces for IPC communication are defined:
* IDemoService: A Hello World sample without usage of zenon API.
* IAlarmService: Designed to return the last selected alarms that have been selected in an alarm message list, by using the SelectionChanged event of alarm message list.


Two extensions are available in project AddInCommunicationSample:
* ProjectServiceExtension: The Add-In Service acts as server by hosting the two services DemoService and AlarmService available for other extensions, WPF elements and external apps.
* ProjectWizardExtension: The Add-In Wizard demonstrates how to consume services like IDemoService and IAlarmService.


There are some reusable components, therefore they are defined in library AddInSampleLibrary
* ServiceHost: Manages the hosting of extension services.
* ServiceClient: Manages consuming a specific extension service.

[back to Top](#contents)

# MvvmEditorWizard
<a name="ExtensionWpfElementInteraction"></a>
This sample Add-In uses MVVM using WPF.

Shows an exemplarily implementation of the [MVVM Pattern](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel) using WPF and zenon Add-Ins. The UI is designed as a classical Wizard, which consists of a main view and several pages. Navigation between pages is implemented using [Commands](https://www.codeproject.com/Articles/25445/WPF-Command-Pattern-Applied). 

There are several folders in this project:

## Assets

All bitmaps or other resource stuff

## ViewModels
Classes that connects Views with the model. The ViewModel of MainView and base classes used for ViewModels (DataModel) and Commands (ActionCommand) are located here.

The sub folder "Pages" contains base classes for page ViewModels and a concrete implementation for each page.

## Views
Views are located in this folder. There is again a sub folder "Pages" that contains all page views.

[back to Top](#contents)

# ObjectCreationWithXmlImport
<a name="ExtensionWpfElementInteraction"></a>

This sample demonstrates how to use the xml import functionality to create zenon objects in the zenon engineering studio. In the example the creation of an equipment model based on a template xml export is demonstrated. Creator classes for other zenon objects can be created likewise.

This comes in quite handy when creating large amounts of zenon objects (e.g. thousands of variables) as the COM interface is only triggered once on importing the prepared xml file.

[back to Top](#contents)

# ThreadingWizardSample
<a name="ExtensionWpfElementInteraction"></a>

This sample demonstrates how to use Threads using WPF Wizards. There are different approaches available in .NET Framework, therefore the Add-In project contains four Engineering Studio Wizard extensions to demonstrate each approach. All four Wizards generate a usage statistic of data types.

* NoThreadWizardExtension: This Wizard does not use a thread. The UI will block the more variables are existing in the project.
* ThreadingWizardExtension: Uses System.Threading.Thread to load data, therefore the UI is non-blocking. The sample uses the WPF Dispatcher to access the UI.
* BackgroundWorkerWizardExtension: Uses System.ComponentModel.BackgroundWorker to load data. The UI is non-blocking. 
* TplWizardExtension: Uses Task Parallel Library (TPL) to load data. The sample is again non-blocking and the recommended approach.

[back to Top](#contents)

# VariableReadWrite
<a name="VariableReadWrite"></a>

This is a very basic but frequently asked-for example on how to read and write values to a variable in zenon. It also shows, that read-only variables cannot be written. Please note that the zenon COM cannot handle ten-thousands of calls per seconds and hence is not useable as alternative value connection. Always use drivers as primarily value source for variables.
For an easier access to this sample a zenon project backup is included. 

[back to Top](#contents)

# VariableSubscriptionSample
<a name="VariableSubscriptionSample"></a>

Demonstrates how to build a reusable class for online containers for variables.

This sample is based on [AddInSampleLibrary](#AddInSampleLibrary).

* AddInSampleLibrary\Subscription\VariableSubscription.cs: Class VariableSubscription is responsible for variable subscription, by creating an online container and handling the bulk changed event. The changed event uses TPL (Task Parallel Library) to ensure that zenon Service Engine gets not blocked during handling a (simulated) long running operation.
* AddInSampleLibrary\ErrorHandler.cs: Is used to check return values of the zenon API to ensure that the operation that has been called was successfully completed.
* AddInSampleLibrary\Logging\NLogConfigurator.cs: Is used log the inner operations of class VariableSubscription and the Add-In extension.

[back to Top](#contents)

