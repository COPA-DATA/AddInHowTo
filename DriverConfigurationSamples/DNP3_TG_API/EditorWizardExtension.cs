using System;
using Scada.AddIn.Contracts;
using DriverCommon;

namespace DNP3_TG_API
{
    /// <summary>
    /// Description of Editor Wizard Extension.
    /// </summary>
    [AddInExtension("DNP3_TG_API", "Test DNP3_TG API, Import and Export", "Drivers API/Export/Import")]
    public class EditorWizardExtension : IEditorWizardExtension
    {
        private Log _log;
        private DriverContext _driverContext;

        const string DriverIdent = "DNP3_TG";
        const string DriverName = "DNP3 third generation Treiber";
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

            _driverContext.SetSignedProperty("DrvConfig.Options.Serial", 0, Int32.MinValue, Int32.MaxValue, false);
            _driverContext.SetStringProperty("DrvConfig.Options.NetworkAdapter", "STRINGPROP", true);
            _driverContext.SetBooleanProperty("DrvConfig.Options.EnableUDP");
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.LocalUDPPort", 0, 65535);
            _driverContext.SetStringProperty("DrvConfig.Options.BroadcastAddress", "STRINGPROP", true);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.BroadcastPort", 0, 65535);
            _driverContext.SetBooleanProperty("DrvConfig.Options.EnableDualEndpoint");
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.TCPListenerPort", 0, 65535);
            _driverContext.SetSignedProperty("DrvConfig.Options.DoublePointMapping", 0, Int32.MinValue, Int32.MaxValue, false);
            _driverContext.SetBooleanProperty("DrvConfig.Options.UTCTime");
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.RCVTimeout", 0, 4294967296);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.SendTimeout", 0, 4294967296);
            _driverContext.SetBooleanProperty("DrvConfig.Options.HalfDuplex");
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.NULOnTime", 0, 9999);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.NULOffTime", 0, 9999);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.TripOnTime", 0, 9999);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.TripOffTime", 0, 9999);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.CloseOnTime", 0, 9999);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.CloseOffTime", 0, 9999);
                 
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

      _driverContext.SetUnsignedProperty(connNamePrefix + "NetAdress", 1, 0, 999, true);
      _driverContext.SetStringProperty(connNamePrefix + "FriendlyName", "Name_" + connIndexString, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "LinkId", 53453, 0, 65535, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "SrcAddress", 53453, 0, 65535, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "DstAddress", 4, 0, 65535, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "ReplyTimeout", 10000, 0, 65535, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "Retries", 3, 0, 65535, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "Keepalive", 120, 0, 65535, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "ErrorWaitTime", 20, 0, 65535, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "IntIv", 3600, 0, 99999, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "InitialWaitTime", 0, 0, 99999, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "EvIv1", 3, 0, 99999, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "EvIv2", 3, 0, 99999, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "EvIv3", 3, 0, 99999, true);
      _driverContext.SetBooleanProperty(connNamePrefix + "Unsol1");    
      _driverContext.SetBooleanProperty(connNamePrefix + "Unsol2");    
      _driverContext.SetBooleanProperty(connNamePrefix + "Unsol3");
      _driverContext.SetSignedProperty(connNamePrefix + "AuthUse", 0, Int32.MinValue, Int32.MaxValue, false);
      _driverContext.SetSignedProperty(connNamePrefix + "AuthKeyWrapType", 0, Int32.MinValue, Int32.MaxValue, false);
      _driverContext.SetStringProperty(connNamePrefix + "AuthUpdateKey", "" + connIndexString, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "AuthSessionTimer", 900, 0, 65535, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "AuthSessionCounter", 1000, 0, 65535, true);
      _driverContext.SetUnsignedProperty(connNamePrefix + "AuthErrorCounter", 2, 0, 65535, true);
      _driverContext.SetSignedProperty(connNamePrefix + "AuthHMACType", 0, Int32.MinValue, Int32.MaxValue, false);
      _driverContext.SetBooleanProperty(connNamePrefix + "AuthAggressiveMode");
      _driverContext.SetBooleanProperty(connNamePrefix + "AuthResponse");
      _driverContext.SetUnsignedProperty(connNamePrefix + "MaxAPDUSize", 2048, 0, 65535, true);
      _driverContext.SetBooleanProperty(connNamePrefix + "TimesyncLAN");
      _driverContext.SetBooleanProperty(connNamePrefix + "NoAutoIIN_ClassPoll");
      _driverContext.SetBooleanProperty(connNamePrefix + "NoAutoIIN_TimeSync");
      _driverContext.SetBooleanProperty(connNamePrefix + "NoAutoIIN_IntPoll");   
      _driverContext.SetBooleanProperty(connNamePrefix + "TimeSyncDelayMeasure");
       _driverContext.SetUnsignedProperty(connNamePrefix + "FileTransferTimeout", 5, 0, 99999999, true);
      _driverContext.SetStringProperty(connNamePrefix + "FileTransferDir", "", true);
      _driverContext.SetStringProperty(connNamePrefix + "FileTransferRevDir", "", true);

      _log.FunctionExitMessage();
    }

    #endregion
  }

}
