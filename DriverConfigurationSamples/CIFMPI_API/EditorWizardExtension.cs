using System;
using Scada.AddIn.Contracts;
using DriverCommon;

namespace CIFMPI_API
{
    /// <summary>
    /// Description of Editor Wizard Extension.
    /// </summary>
    [AddInExtension("CIFMPI_API", "Test CIFMPI API, Import and Export", "Drivers API/Export/Import")]
    public class EditorWizardExtension : IEditorWizardExtension
    {
        private Log _log;
        private DriverContext _driverContext;

        const string DriverIdent = "CIFMPI";
        const string DriverName = "Hilscher MPI Treiber";
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

            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.Version", 0, 10000);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.Karte", 0, 100);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.Adresse", 0, 100);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.Timeout", 10, 100000);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.Baudrate", 0, 100);
            _driverContext.SetBooleanProperty("DrvConfig.Options.StringHeader");
            _driverContext.SetUnsignedProperty("DrvConfig.Options.KartenVerbindung", 0, 1);
            _driverContext.IncreaseUnsignedProperty("DrvConfig.Options.IPAdresse", UInt32.MinValue, UInt32.MaxValue);
            _driverContext.SetUnsignedProperty("DrvConfig.Options.Karte", 0, 1);

            _log.FunctionExitMessage();
        }

        #endregion
    }

}
