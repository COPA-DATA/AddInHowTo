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

            _driverContext.SetBooleanProperty("DrvConfig.Options.DoNotExtractPropertyFromAddress");
            _driverContext.SetBooleanProperty("DrvConfig.Options.RegisterAsForeignDevice");

            _log.FunctionExitMessage();
        }

        #endregion
    }

}
