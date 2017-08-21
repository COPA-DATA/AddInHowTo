using System;

namespace CommunicationLibrary
{
    /// <summary>
    /// A serializable class to exchange alarm data through the IPC communication interface
    /// </summary>
    [Serializable]
    public class AlarmData
    {
        /// <summary>
        /// Alaram text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The corresponding variable
        /// </summary>
        public string VariableName { get; set; }
    }
}
