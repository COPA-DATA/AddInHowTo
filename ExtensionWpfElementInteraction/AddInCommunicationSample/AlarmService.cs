using System;
using System.Linq;
using CommunicationLibrary;
using Scada.AddIn.Contracts;

namespace AddInCommunicationSample
{
    /// <summary>
    /// Implements the alarm service
    /// </summary>
    public class AlarmService : MarshalByRefObject, IAlarmService
    {
        private AlarmData[] _lastSelectedAlarmList;

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="project">The zenon project</param>
        internal AlarmService(IProject project)
        {
            project.AlarmMessageList.SelectionChanged += (sender, args) =>
            {
                _lastSelectedAlarmList =
                    args.SelectedItems?.Select(a => new AlarmData {Text = a.Text, VariableName = a.VariableName}).ToArray();
            };
        }

        public override object InitializeLifetimeService()
        {
            // Ensure that the service is not released
            return null;
        }

        public AlarmData[] GetLastSelectedAlarmList()
        {
            return _lastSelectedAlarmList;
        }
    }
}
