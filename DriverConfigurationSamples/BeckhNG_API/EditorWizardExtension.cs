using System;
using Scada.AddIn.Contracts;
using DriverCommon;

namespace BeckhNG_API
{
    /// <summary>
    /// Description of Editor Wizard Extension.
    /// </summary>
    [AddInExtension("BeckhNG_API", "Test BeckhNG API, Import and Export", "Drivers API/Export/Import")]
    public class EditorWizardExtension : IEditorWizardExtension
    {
        private Log _log;
        private DriverContext _driverContext;

        const string DriverIdent = "BeckhNG";
        const string DriverName = "Beckhoff TwinCat NG Treiber";
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

            _driverContext.IncreaseSignedProperty("DrvConfig.Options.Timeout", 0, 3600000);
            _driverContext.IncreaseSignedProperty("DrvConfig.Options.SymbVarName", 0, 2);

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
                ModifyConnection(idxI);
            }

            _log.FunctionExitMessage();

            if (_driverContext.AddNode("DrvConfig.Connections[" + idxI.ToString() + "]"))
            {
                //ModifyConnection(idxI);
            }
        }

        private void ModifyConnection(uint connIndex)
        {
            string connNamePrefix;
            connNamePrefix = "DrvConfig.Connections[" + connIndex.ToString() + "].";

            connIndex = connIndex + 1;
            string cis = connIndex.ToString();

            _log.FunctionEntryMessage($"modify {connIndex}. connection");

            _driverContext.SetUnsignedProperty(connNamePrefix + "NetAddress", connIndex * 2, 1, 65535, true);
            _driverContext.SetUnsignedProperty(connNamePrefix + "Port", connIndex, 1, 65535, true);
            _driverContext.SetStringProperty(connNamePrefix + "NetID", $"{cis}.{cis}.{cis}.{cis}.{cis}.{cis}", true);

            _log.FunctionExitMessage();
        }

        #endregion
    }

}
