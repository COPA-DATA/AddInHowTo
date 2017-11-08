using System;
using Scada.AddIn.Contracts;
using DriverCommon;

namespace BURPVI_API
{
    /// <summary>
    /// Description of Editor Wizard Extension.
    /// </summary>
    [AddInExtension("BURPVI_API", "Test BURPVI API, Import and Export", "Drivers API/Export/Import")]
    public class EditorWizardExtension : IEditorWizardExtension
    {
        private Log _log;
        private DriverContext _driverContext;

        const string DriverIdent = "BURPVI";
        const string DriverName = "BuR-PVI Treiber NG";
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
                    ModifyPVIInit();
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

            _driverContext.SetStringProperty("DrvConfig.Options.KonfDatei", "BurPVI - KonfDatei.txt", true);
            _driverContext.IncreaseSignedProperty("DrvConfig.Options.BAImportMode", 0, 1000);
            _driverContext.SetStringProperty("DrvConfig.Options.BASuffix", "_xyz", true);
            _driverContext.SetBooleanProperty("DrvConfig.Options.BAItemsAsBlockArray");
            _driverContext.SetBooleanProperty("DrvConfig.Options.StructItemsAsBlockArray");

            _log.FunctionExitMessage();
        }

        private void ModifyPVIInit()
        {
            _log.FunctionEntryMessage("modify PVIInit");

            //_driverContext.SetBooleanProperty("DrvConfig.PVIInit.Event");
            _driverContext.IncreaseUnsignedProperty("DrvConfig.PVIInit.Timeout", 0, 1000);
            _driverContext.IncreaseSignedProperty("DrvConfig.PVIInit.LM", 0x8000, 0x7fff);
            _driverContext.IncreaseSignedProperty("DrvConfig.PVIInit.PT", 0x8000, 0x7fff);
            //_driverContext.SetBooleanProperty("DrvConfig.PVIInit.Remote");
            _driverContext.SetStringProperty("DrvConfig.PVIInit.RemoteIP", "1.2.3.4", true);
            _driverContext.IncreaseSignedProperty("DrvConfig.PVIInit.RemotePort", 1, 0xc000);
            _driverContext.SetStringProperty("DrvConfig.PVIInit.Linie", "Linie", true);

            _log.FunctionExitMessage();
        }

        private void ModifyConnections()
        {
            _log.FunctionEntryMessage("modify connections");

            string[] propItems;
            uint connCount;
            _driverContext.GetNodeInfo("DrvConfig.Connections", out propItems, out connCount);

            uint idxI;
            for (idxI = 0; idxI < connCount; idxI++)
            {
                ModifyConnection(idxI, true);
            }

            _log.FunctionExitMessage();

            // add a new connection (not using an index)
            if (_driverContext.AddNode("DrvConfig.Connections"))
            {
                // this connection remains empty and will not be accepted by the driver!

                idxI += 1;
                // add a new connection - uses an index
                if (_driverContext.AddNode("DrvConfig.Connections[" + idxI.ToString() + "]"))
                {
                    // this connection gets the minimum information necessary to be accepted by the driver
                    ModifyConnection(idxI, false);
                }
            }
        }

        private void ModifyConnection(uint connIndex, bool bComplete)
        {
            string connNamePrefix;
            string connIndexString = connIndex.ToString();
            connNamePrefix = "DrvConfig.Connections[" + connIndexString + "].";

            connIndex = connIndex + 1;

            _log.FunctionEntryMessage($"modify {connIndex}. connection");

            _driverContext.SetUnsignedProperty(connNamePrefix + "NetAddress", connIndex * 2, 0, 255, true);
            _driverContext.SetStringProperty(connNamePrefix + "Name", "Name_" + connIndexString, true);
            _driverContext.SetStringProperty(connNamePrefix + "DeviceParameter", "DevPrm_" + connIndexString, true);
            if (bComplete)
            {
                _driverContext.SetStringProperty(connNamePrefix + "CPUParameter", "CpuPrm_" + connIndexString, true);
                _driverContext.SetStringProperty(connNamePrefix + "PathTarget", "Target_" + connIndexString, true);
            }

            _log.FunctionExitMessage();
        }

        #endregion
    }

}
