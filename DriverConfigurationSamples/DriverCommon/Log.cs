using System;
using AddInSampleLibrary.Logging;
using NLog;
using Scada.AddIn.Contracts;

namespace DriverCommon
{
    public class Log
    {
        private readonly IEditorApplication _editorApplication;
        private readonly string _driverApiName;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Log(IEditorApplication editorApplication, string driverIdent)
        {
            var configurator = new NLogConfigurator();
            configurator.Configure();

            _editorApplication = editorApplication;
            _driverApiName = driverIdent + "API";
        }

        public void Message(string msgText)
        {
            string text = $" - [{_driverApiName}]: {msgText}";
            _editorApplication.DebugPrint(text, DebugPrintStyle.Standard);
            Logger.Info(text);
        }

        public void InvalidPropertyValueMessage(string propName)
        {
            string text = $" - [{_driverApiName}]:     [{propName}] is invalid (and remains invalid)";
            _editorApplication.DebugPrint(text, DebugPrintStyle.Standard);
            Logger.Warn(text);
        }

        public void ExpectionMessage(string msgText, Exception ex)
        {
            string text = $" - [{_driverApiName}]:     [{msgText}] (Exception: {ex.Message})";
            _editorApplication.DebugPrint(text, DebugPrintStyle.Standard);
            Logger.Error(ex, text);
        }

        public void FunctionEntryMessage(string msgText)
        {
            _editorApplication.DebugPrint($" - [{_driverApiName}]:   {msgText}", DebugPrintStyle.Standard);
        }
        public void FunctionExitMessage()
        {
        }
   
        public void PropertyModifiedMessage(string propName, object orgValue, object newValue, string propType)
        {
            _editorApplication.DebugPrint($" - [{_driverApiName}]:    [{propName}] from [{orgValue}] to [{newValue}] (value type: {propType})", DebugPrintStyle.Standard);
        }
     }
}
