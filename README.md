zenon is a software system from COPA-DATA for industrial automation and the energy industry. Machines and equipment are controlled, monitored and optimized. zenon's particular strength is open and reliable communication throughout heterogeneous production facilities. Over 300 native communication protocols support the horizontal and vertical exchange of data. This allows for the continuous implementation of Industrial IoT and the Smart Factory.

zenon’s engineering environment is flexible and can be used in many ways. Complex functions for comprehensive applications such as HMI/SCADA and reporting are supplied out of the box to create intuitive and robust applications. Users can thus contribute to the increased flexibility and efficiency of HMI applications using zenon.

To open projects of this repository please install Visual Studio Developer Tools, they available at [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=vs-publisher-1463468.COPA-DATASCADAAdd-InDeveloperToolsforVS) 

This repository contains frequently ask sample of zenon Add-In solutions. All projects of this folder are added to solution [HowTo.sln](HowTo.sln).

# AddInSampleLibrary
<a name="AddInSampleLibrary"></a>
This project demonstrates show to build custom libraries to share common code between zenon Add-Ins. Following common classes are added to this project:

* Subscription\VariableSubscription.cs: Class VariableSubscription is responsible for variable subscription, by creating an online container and handling the bulk changed event. The changed event uses TPL (Task Parallel Library) to ensure that zenon Runtime gets not blocked during handling a (simulated) long running operation.
* Logging\NLogConfigurator.sln: A reusable class to add logging functionality to Add-Ins. This code is derived from the training sample [NLogSample](#NLogSample)
* Communication: Reusable classes to interact between Add-In Extensions, WPF Elements or external code. This sample is documentated [here](#ExtensionWpfElementInteraction)
* ErrorHandler.cs: Is used to check return values of the zenon API to ensure that the operation that has been called was successfully completed.

# ExtensionWpfElementInteraction
<a name="ExtensionWpfElementInteraction"></a>
This sample demonstrates how to communicate between Add-In Extensions and WPF Elements. Every extension is separated using AppDomains therefore a direct interaction between Add-In Extensions and also WPF Elements is not possible by design. Using Inter Process Communication (IPC) allows to
share data between Add-Ins.

The library called "CommunicationLibrary" contains code that is shared between Add-Ins and WPF elements. Here two interfaces for IPC communication are defined:
* IDemoService: A Hello World sample without usage of zenon API.
* IAlarmService: Designed to return the last selected alarms that have been selected in an alarm message list, by using the SelectionChanged event of alarm message list.


Two extensions are available in project AddInCommunicationSample:
* ProjectServiceExtension: The Add-In Service acts as server by hosting the two services DemoService and AlarmService available for other extensions, WPF elements and external apps.
* ProjectWizardExtension: The Add-In Wizard demonstrates how to consume srvices like IDemoService and IAlarmService.


There are some reusable components, therefore they are defined in library AddInSampleLibrary
* ServiceHost: Manages the hosting of extension services.
* ServiceClient: Manages consuming a specific extension service.

# MvvmEditorWizard
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

# ObjectCreationWithXmlImport
This sample demonstrates how to use the xml import functionality to create zenon objects in the zenon engineering studio. In the example the creation of an equipment model based on a template xml export is demonstrated. Creator classes for other zenon objects can be created likewise.

This comes in quite handy when creating large amounts of zenon objects (e.g. thousands of variables) as the COM interface is only triggered once on importing the prepared xml file.

# ThreadingWizardSample
This sample demonstrates how to use Threads using WPF Wizards. There are different approaches available in .NET Framework, therefore the Add-In project contains four Editor Wizard extensions to demonstrate each approach. All four Wizards generate a usage statistic of data types.

* NoThreadWizardExtension: This Wizard does not use a thread. The UI will block the more variables are existing in the project.
* ThreadingWizardExtension: Uses System.Threading.Thread to load data, therefore the UI is non-blocking. The sample uses the WPF Dispatcher to access the UI.
* BackgroundWorkerWizardExtension: Uses System.ComponentModel.BackgroundWoker to load data. The UI is non-blocking. 
* TplWizardExtension: Uses Task Parallel Library (TPL) to load data. The sample is again non-blocking and the recommended approach.


# UnitTestsSample
Contains a Unit Test project, that demonstrates how to unit test classes that is using zenon API. The zenon API is simulated by using [Moq](https://github.com/moq/moq). A getting started guide for Moq is available [here](https://github.com/Moq/moq4/wiki/Quickstart). 

This sample is based on [AddInSampleLibrary](#AddInSampleLibrary).


# VariableSubscriptionSample
Demonstrates how to build a reusable class for online containers for variables.

This sample is based on [AddInSampleLibrary](#AddInSampleLibrary).

* AddInSampleLibrary\Subscription\VariableSubscription.cs: Class VariableSubscription is responsible for variable subscription, by creating an online container and handling the bulk changed event. The changed event uses TPL (Task Parallel Library) to ensure that zenon Runtime gets not blocked during handling a (simulated) long running operation.
* AddInSampleLibrary\ErrorHandler.cs: Is used to check return values of the zenon API to ensure that the operation that has been called was successfully completed.
* AddInSampleLibrary\Logging\NLogConfigurator.cs: Is used log the inner operations of class VariableSubscription and the Add-In extension.

