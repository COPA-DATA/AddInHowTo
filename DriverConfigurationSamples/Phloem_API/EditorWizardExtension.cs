using System;
using Scada.AddIn.Contracts;
using DriverCommon;

namespace Phloem_API
{
    /// <summary>
    /// Description of Editor Wizard Extension.
    /// </summary>
    [AddInExtension("Phloem_API", "Test PHLOEM API, Import and Export", "Drivers API/Export/Import")]
    public class EditorWizardExtension : IEditorWizardExtension
    {
        private Log _log;
        private DriverContext _driverContext;

        const string DriverIdent = "PHLOEM";
        const string DriverName = "Phloem";
        const string XmlSuffixBefore = "before";
        const string XmlSuffixAfter = "after";

        #region IEditorWizardExtension implementation

        public void Run(IEditorApplication context, IBehavior behavior)
        {
            _log = new Log(context, DriverIdent);

            try
            {
                _driverContext = new DriverContext(context, _log, DriverName, false);

                // enter your code which should be executed when starting the SCADA Editor Wizard

                _log.Message("begin test");

                _driverContext.Export(XmlSuffixBefore);

                if (_driverContext.OpenDriver(10))
                {
                    _driverContext.ModifyCommonProperties();
                    _driverContext.ModifyCOMProperties();

                    ModifyOptions();
                    ModifyConnections();

                    _driverContext.CloseDriver();

                    _driverContext.Export(XmlSuffixAfter);
                    _driverContext.Import(XmlSuffixBefore);
                }

                _log.Message("end test");
            }
            catch (Exception ex)
            {
                _log.ExpectionMessage($"An exception has been thrown: {ex.Message}", ex);
                throw;
            }
        }

        private void ModifyOptions()
        {
            _log.FunctionEntryMessage("modify options");

            _driverContext.SetBooleanProperty("DrvConfig.Options.Serial");

            _log.FunctionExitMessage();
        }

        private void ModifyConnections()
        {
            _log.FunctionEntryMessage("modify connections");

            string[] propItems;
            uint connCount;
            _driverContext.GetNodeInfo("DrvConfig.Connections", out propItems, out connCount);

            for (uint idxI = 0; idxI < connCount; idxI++)
            {
                ModifyConnection(idxI);
            }

            _log.FunctionExitMessage();
        }

        private void ModifyConnection(uint connIndex)
        {
            string connNamePrefix;
            connNamePrefix = "DrvConfig.Connections[" + connIndex.ToString() + "].";

            connIndex = connIndex + 1;

            _log.FunctionEntryMessage($"modify {connIndex}. connection");

            _driverContext.SetUnsignedProperty(connNamePrefix + "NetAddress", connIndex, 0, 65535, true);
            _driverContext.SetStringProperty(connNamePrefix + "ConnectionName", "connection #" + connIndex.ToString(), true);
            _driverContext.SetSignedProperty(connNamePrefix + "KnownID", (Int32)(connIndex), 0, 65535, false);
            _driverContext.SetStringProperty(connNamePrefix + "IPAddress", $"{connIndex}.{connIndex}.{connIndex}.{connIndex}", false);
            _driverContext.SetUnsignedProperty(connNamePrefix + "IPPort", connIndex * 1000, 1, 65535, false);

            _log.FunctionExitMessage();
        }

        #endregion
    }

}
