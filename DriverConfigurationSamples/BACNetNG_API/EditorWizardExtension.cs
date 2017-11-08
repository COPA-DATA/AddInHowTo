using System;
using Scada.AddIn.Contracts;
using DriverCommon;

namespace BACNetNG_API
{
    /// <summary>
    /// Description of Editor Wizard Extension.
    /// </summary>
    [AddInExtension("BACNetNG_API", "Test BACNetNG API, Import and Export", "Drivers API/Export/Import")]
    public class EditorWizardExtension : IEditorWizardExtension
    {
        private Log _log;
        private DriverContext _driverContext;

        const string DriverIdent = "BACNetNG";
        const string DriverName = "BACnet Treiber Next Generation";
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
                    _driverContext.DumpNodeInfo("DrvConfig.Options");

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

            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.NetworkNumber", 0, 255);
            _driverContext.SetStringProperty("DrvConfig.Options.Connection", "STRINGPROP", true);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.UDPPort", 0x80, 0xc000);
            _driverContext.SetCharacterProperty("DrvConfig.Options.PropertySeparator", 'ä');
            _driverContext.SetStringProperty("DrvConfig.Options.UDPBroadcastPorts", "1234", true);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.ErrorWaitTime_ms", 100, 1000000);
            _driverContext.IncreaseSignedProperty("DrvConfig.Options.SymAddressSourceProperty", -1000000, 1000000);
            _driverContext.SetBooleanProperty("DrvConfig.Options.DoNotExtractPropertyFromAddress");
            _driverContext.SetCharacterProperty("DrvConfig.Options.ObjectSeperator", 'ö');
            _driverContext.SetBooleanProperty("DrvConfig.Options.RegisterAsForeignDevice");
            _driverContext.SetStringProperty("DrvConfig.Options.ForeignDeviceBBMDAddress", "1.2.3.4", true);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.ForeignDeviceBBMDPort", 0x80, 0xc000);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.ForeignDeviceRegistrationLifeTime_s", 0, 0xffff);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.ForeignDeviceRegistrationTimeout_ms", 100, 1000000);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.ForeignDeviceRegistrationRetries", 1, 99);

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

      // add a new connection (not using an index)
      if (_driverContext.AddNode("DrvConfig.Connections"))
      {
        // this connection remains with default values

        idxI += 1;
        // add a new connection - uses an index
        if (_driverContext.AddNode("DrvConfig.Connections[" + idxI.ToString() + "]"))
        {
          // this connection gets the minimum information necessary to be accepted by the driver
          ModifyConnection(idxI);
        }
      }
    }

    private void ModifyConnection(uint connIndex)
    {
      string connNamePrefix;
      string connIndexString = connIndex.ToString();
      connNamePrefix = "DrvConfig.Connections[" + connIndexString + "].";

      connIndex = connIndex + 1;

      _log.FunctionEntryMessage($"modify {connIndex}. connection");

      _driverContext.SetUnsignedProperty(connNamePrefix + "ProcessId", connIndex * 2, 0, 255, true);
      _driverContext.SetStringProperty(connNamePrefix + "DeviceName", "Name_" + connIndexString, true);
      _driverContext.SetBooleanProperty(connNamePrefix + "ManualAddress");
      _driverContext.SetBooleanProperty(connNamePrefix + "ReadAPDUTimeouts");
      _driverContext.SetUnsignedProperty(connNamePrefix + "APDURetries", 15, 0, 255, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "CharacterEncoding", 2, 0, 255, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "MaxAPDUSize", 999, 0, 65535, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "MaxConcurrentRequests", 256, 0, 65535, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "APDUTimeout", 111, 0, 4294967295, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "APDUSegmentTimeout", 222, 0, 4294967295, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "COVLifetime", 333, 0, 4294967295, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "COVResubscriptionConst", 444, 0, 4294967295, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "TrendLogPollingInterval", 555, 0, 4294967295, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "NetworkNumber", 9, 0, 65535, true);
      _driverContext.SetStringProperty(connNamePrefix + "RouterAddress", "01:00:00:01:00:01", true);
      _driverContext.SetStringProperty(connNamePrefix + "DeviceAddress", "01:00:00:01:00:02", true);

      _log.FunctionExitMessage();
    }

    #endregion
  }

}
