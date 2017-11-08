using System;
using Scada.AddIn.Contracts;
using DriverCommon;

namespace OPCUA32_API
{
    /// <summary>
    /// Description of Editor Wizard Extension.
    /// </summary>
    [AddInExtension("OPCUA32_API", "DriverContext OPCUA32 API, Import and Export", "Drivers API/Export/Import")]
    public class EditorWizardExtension : IEditorWizardExtension
    {
        private Log _log;
        private DriverContext _driverContext;

        const string DriverIdent = "OPCUA32";
        const string DriverName = "OPC UA Client Treiber";
        const string SuffixBefore = "before";
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

                _driverContext.Export(SuffixBefore);

                if (_driverContext.OpenDriver(10))
                {
                    _driverContext.ModifyCommonProperties();
                    _driverContext.ModifyCOMProperties();

                    //ModifyOptions();
                    ModifyServers();

                    _driverContext.CloseDriver();

                    _driverContext.Export(XmlSuffixAfter);
                    _driverContext.Import(SuffixBefore);
                }

                _log.Message("end test");
            }
            catch (Exception ex)
            {
                _log.ExpectionMessage($"An exception has been thrown: {ex.Message}", ex);
                throw;
            }
        }

        //private void ModifyOptions()
        //{
        //    _log.FunctionEntryMessage("modify options");

        //    _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.MaxNodesPerRead", 1, 1000);
        //    _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.MaxNodesPerWrite", 1, 1000);
        //    _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.MaxNodesPerBrowse", 1, 1000);
        //    _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.MaxNodesPerTranslate", 1, 1000);
        //    _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.MaxMonitoredItemsPerCall", 1, 1000);

        //    _log.FunctionExitMessage();
        //}

        private void ModifyServers()
        {
            _log.FunctionEntryMessage("modify servers");

            string[] propItems;
            uint srvrCount;
            _driverContext.GetNodeInfo("DrvConfig.Server", out propItems, out srvrCount);

            for (uint idxI = 0; idxI < srvrCount; idxI++)
            {
                ModifyServer(idxI);
            }

            _log.FunctionExitMessage();
        }

        private void ModifyServer(uint srvrIndex)
        {
            var srvrNamePrefix = "DrvConfig.Server[" + srvrIndex + "].";

            srvrIndex = srvrIndex + 1;

            _log.FunctionEntryMessage($"modify {srvrIndex}. server");

            _driverContext.SetUnsignedProperty(srvrNamePrefix + "NetAddress", srvrIndex, 0, 65535, true);
            //_driverContext.SetBooleanProperty(srvrNamePrefix + "StorePassword");
            _driverContext.SetStringProperty(srvrNamePrefix + "Username", "555", true);
            _driverContext.SetStringProperty(srvrNamePrefix + "Password", "666", true);
            _driverContext.IncreaseUnsignedProperty(srvrNamePrefix + "SecurityMode", 1, 3);

            //ModifyServerCommunication(srvrNamePrefix);
            ModifyServerProtocol(srvrNamePrefix);
            ModifyServerOpLimits(srvrNamePrefix);

            _log.FunctionExitMessage();
        }

        //private void ModifyServerCommunication(string csPathname)
        //{
        //    var commNamePrefix = csPathname + "Communication.";

        //    _log.FunctionEntryMessage("modify communication");

        //    _driverContext.IncreaseDoubleProperty(commNamePrefix + "PublishInterval", -100000.0, 100000.0);
        //    _driverContext.IncreaseUnsignedProperty(commNamePrefix + "LifeTimeCount", 1, 100000);
        //    _driverContext.IncreaseUnsignedProperty(commNamePrefix + "KeepAliveCount", 1, 1000);
        //    _driverContext.IncreaseDoubleProperty(commNamePrefix + "SamplingInterval", -100000.0, 100000.0);

        //    _log.FunctionExitMessage();
        //}

        private void ModifyServerProtocol(string csPathname)
        {
            var protNamePrefix = csPathname + "Protocol.";

            _log.FunctionEntryMessage("modify protocol");

            _driverContext.IncreaseDoubleProperty(protNamePrefix + "PublishInterval", -100000.0, 100000.0);
            _driverContext.IncreaseUnsignedProperty(protNamePrefix + "LifeTimeCount", 1, 100000);
            _driverContext.IncreaseUnsignedProperty(protNamePrefix + "KeepAliveCount", 1, 1000);
            _driverContext.IncreaseDoubleProperty(protNamePrefix + "SamplingInterval", -100000.0, 100000.0);
            _driverContext.IncreaseUnsignedProperty(protNamePrefix + "DataChangeFilter", 0, 1);
            _driverContext.SetBooleanProperty(protNamePrefix + "AbsoluteDeadband");
            _driverContext.SetBooleanProperty(protNamePrefix + "PersistentNodeIDs");
            _driverContext.SetBooleanProperty(protNamePrefix + "ReadInitialValues");
            _driverContext.SetBooleanProperty(protNamePrefix + "ArraysOnIndexBasis");

            _log.FunctionExitMessage();
        }
        private void ModifyServerOpLimits(string csPathname)
        {
            var protNamePrefix = csPathname + "OperationLimits.";

            _log.FunctionEntryMessage("modify operation limits");

            _driverContext.IncreaseUnsignedProperty(protNamePrefix + "MaxNodesPerRead", 1, 1000);
            _driverContext.IncreaseUnsignedProperty(protNamePrefix + "MaxNodesPerWrite", 1, 1000);
            _driverContext.IncreaseUnsignedProperty(protNamePrefix + "MaxNodesPerBrowse", 1, 1000);
            _driverContext.IncreaseUnsignedProperty(protNamePrefix + "MaxNodesPerTranslate", 1, 1000);
            _driverContext.IncreaseUnsignedProperty(protNamePrefix + "MaxMonitoredItemsPerCall", 1, 1000);
            _driverContext.SetBooleanProperty(protNamePrefix + "UseServerOperationLimits");

            _log.FunctionExitMessage();
        }


        #endregion
    }

}
