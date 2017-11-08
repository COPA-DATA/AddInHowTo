using System;
using Scada.AddIn.Contracts;
using DriverCommon;

namespace D3S_V3_API
{
    /// <summary>
    /// Description of Editor Wizard Extension.
    /// </summary>
    [AddInExtension("3S_V3_API", "Test 3S_V3 API, Import and Export", "Drivers API/Export/Import")]
    public class EditorWizardExtension : IEditorWizardExtension
    {
        private Log _log;
        private DriverContext _driverContext;

        const string DriverIdent = "3S_V3";
        const string DriverName = "3S v3 Treiber für PLC Handler";
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

            _driverContext.IncreaseSignedProperty("DrvConfig.Options.SymbVarName", 0, 2);
            _driverContext.IncreaseSignedProperty("DrvConfig.Options.Timestamp", 0, 1);

            _log.FunctionExitMessage();
        }

        private void ModifyConnections()
        {
            _log.FunctionEntryMessage("modify connections");

            string[] propItems;
            uint connCount;
            _driverContext.GetNodeInfo("DrvConfig.Connection", out propItems, out connCount);

            for (uint idxI = 0; idxI < connCount; idxI++)
            {
                ModifyConnection(idxI);
            }

            _log.FunctionExitMessage();
        }

        private void ModifyConnection(uint connIndex)
        {
            string connNamePrefix;
            connNamePrefix = "DrvConfig.Connection[" + connIndex.ToString() + "].";

            connIndex = connIndex + 1;

            _log.FunctionEntryMessage($"modify {connIndex}. connection");

            _driverContext.SetUnsignedProperty(connNamePrefix + "HWAddress", 1000 + connIndex, 1000, 1999, true);
            _driverContext.IncreaseUnsignedProperty(connNamePrefix + "ConnectionType", 0, 2);
            _driverContext.IncreaseUnsignedProperty(connNamePrefix + "PLCVersion", 0, 1);
            _driverContext.IncreaseUnsignedProperty(connNamePrefix + "GWConnection", 0, 2);

            _driverContext.SetStringProperty(connNamePrefix + "Address", "Address #" + connIndex.ToString(), true);
            _driverContext.SetStringProperty(connNamePrefix + "NodeAddress", "NodeAddress #" + connIndex.ToString(), true);
            _driverContext.SetStringProperty(connNamePrefix + "HostAddress", "HostAddress #" + connIndex.ToString(), true);
            _driverContext.SetStringProperty(connNamePrefix + "AddressSecondary", "Addr 2nd #" + connIndex.ToString(), true);
            _driverContext.SetStringProperty(connNamePrefix + "NodeAddressSecondary", "NAddr 2nd #" + connIndex.ToString(), true);
            _driverContext.SetStringProperty(connNamePrefix + "HostAddressSecondary", "HAddr 2nd #" + connIndex.ToString(), true);
            _driverContext.SetStringProperty(connNamePrefix + "SelectedConnectionVarName", "SCVN #" + connIndex.ToString(), true);
            _driverContext.SetStringProperty(connNamePrefix + "GWAddress", "GWAddress #" + connIndex.ToString(), true);
            _driverContext.SetStringProperty(connNamePrefix + "GWPassword", "GWPassword #" + connIndex.ToString(), true);
            _driverContext.SetStringProperty(connNamePrefix + "Alias", "Alias #" + connIndex.ToString(), true);

            _driverContext.SetUnsignedProperty(connNamePrefix + "GWPort", 2000 + connIndex, 1, 65535, true);
            _driverContext.IncreaseUnsignedProperty(connNamePrefix + "BufferSize", 100, 65535);

            _driverContext.IncreaseUnsignedProperty(connNamePrefix + "V23Transport", 0, 3);
            _driverContext.SetUnsignedProperty(connNamePrefix + "V23Port", 3000 + connIndex, 1, 65535, true);
            _driverContext.SetUnsignedProperty(connNamePrefix + "V23Target", 4000 + connIndex, 1, 65535, true);
            _driverContext.IncreaseUnsignedProperty(connNamePrefix + "V23BufferSize", 100, 65535);
            _driverContext.IncreaseUnsignedProperty(connNamePrefix + "V23Motorola", 0, 1);

            _driverContext.IncreaseUnsignedProperty(connNamePrefix + "Timeout", 1, 1000000);
            _driverContext.IncreaseUnsignedProperty(connNamePrefix + "Retries", 1, 10);

            _log.FunctionExitMessage();
        }

        #endregion
    }

}
