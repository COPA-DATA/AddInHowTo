using System;
using System.Runtime.Remoting;
using System.Windows.Forms;
using AddInSampleLibrary.Communication;
using CommunicationLibrary;
using Scada.AddIn.Contracts;

namespace AddInCommunicationSample
{
    /// <summary>
    /// Represents a runtime wizard that consumes data from the communication service
    /// </summary>
    [AddInExtension("Data consumer demo", "Demo how to communicate between extensions. This extension represents the 'client' side.")]
    public class ProjectWizardExtension : IProjectWizardExtension
    {
        public void Run(IProject context, IBehavior behavior)
        {
            try
            {
                ShowHelloWorld();
                ShowSelectedAlarams();
            }
            catch (RemotingException)
            {
                MessageBox.Show("Please ensure that Data Provider Service is started.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private static void ShowHelloWorld()
        {
            using (var demoServiceClient = new ServiceClient<IDemoService>(Constants.CommunicationServiceEndPointName))
            {
                demoServiceClient.Open(nameof(IDemoService));

                MessageBox.Show(demoServiceClient.ServiceProxy.GetHelloWorldMessage());
            }
        }

        private static void ShowSelectedAlarams()
        {
            AlarmData[] lastSelectedAlarmList;
            using (var communicationServiceClient = new ServiceClient<IAlarmService>(Constants.CommunicationServiceEndPointName))
            {
                communicationServiceClient.Open(nameof(IAlarmService));

                lastSelectedAlarmList = communicationServiceClient.ServiceProxy.GetLastSelectedAlarmList();
            }

            if (lastSelectedAlarmList == null)
            {
                MessageBox.Show("No alarms selected.");
            }
            else
            {
                // Show selected alarms using a message box
                string text = "";
                foreach (AlarmData alarmData in lastSelectedAlarmList)
                {
                    if (!string.IsNullOrEmpty(text))
                    {
                        text += Environment.NewLine;
                    }

                    var alarm = alarmData;
                    text += $"{alarm.VariableName}: {alarm.Text}";
                }

                MessageBox.Show("Selected Alarms:" + Environment.NewLine + text);
            }
        }
    }
}