using System;
using System.IO;
using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.Variable;

namespace DriverCommon
{
    public class DriverContext
    {
        private readonly IDriver _driverObject;
        private readonly Log _log;


        private static string _xmlFolderPath;

        public DriverContext(IEditorApplication editorApplication, Log log, string driverName, bool useTempFolder)
        {
            _log = log;

            if (useTempFolder)
            {
                _xmlFolderPath = Environment.GetEnvironmentVariable("TEMP") + "\\TestDriverAPI\\";
            }
            else
            {
                _xmlFolderPath = Path.GetFullPath(".\\XMLData\\");
            }

            IWorkspace iWorkSpace = editorApplication.Workspace;
            IProject iProject = iWorkSpace.ActiveProject;
            IDriverCollection iDrivers = iProject.DriverCollection;
            _driverObject = iDrivers[driverName];
        }

        public bool OpenDriver(int timeoutInMs)
        {
            bool bFirst = true;
            DateTime startTime;
            DateTime endTime = DateTime.Now.AddMilliseconds(timeoutInMs);

            do
            {
                startTime = DateTime.Now;

                if (_driverObject.InitializeConfiguration())
                {
                    _log.Message($" Opened driver[{ _driverObject.Identification}]");
                    return true;
                }
                if (bFirst)
                {
                    bFirst = false;
                    if (_driverObject.EndConfiguration(true))
                    {
                        _log.Message($" Closed driver [{_driverObject.Identification}] - was previously open");
                    }
                }
                System.Threading.Thread.Sleep(100);
            } while (startTime < endTime);

            _log.Message($" Could not open driver [{_driverObject.Identification}]");
            return false;
        }

        public void CloseDriver()
        {
            _driverObject.EndConfiguration(true);
            _log.Message($" Closed driver [{_driverObject.Identification}]");
        }

        public void DumpNodeInfo(string propertyName)
        {
            _log.Message($"  Dump node [{propertyName}]");
            string[] propItems = _driverObject.GetDynamicProperties(propertyName);
            _log.Message($"    has the following items");
            foreach(string cItem in propItems)
            {
                char[] delimArr = { ',' };
                string[] cValues = cItem.Split(delimArr, 5);
                _log.Message($"      Item [{cValues[0]}] has type [{cValues[1]}] - {cValues[2]}");
            }
            uint connCount = Convert.ToUInt32(_driverObject.GetDynamicProperty(propertyName));
            _log.Message($"    Node [{propertyName}] has {connCount} instances");
        }

        public void GetNodeInfo(string propertyName, out string[] dynamicPropertyNames, out uint count)
        {
            dynamicPropertyNames = _driverObject.GetDynamicProperties(propertyName);
            count = Convert.ToUInt32(_driverObject.GetDynamicProperty(propertyName));
        }

        public bool AddNode(string propertyName)
        {
            bool bResult = false;
            try
            {
                if (_driverObject.CreateDynamicProperty(propertyName) == true)
                {
                    _log.Message($"  created new property [{propertyName}]");
                    bResult = true;
                }
                else
                {
                    _log.Message($"  could not create new property [{propertyName}]");
                }
            }
            catch (Exception ex)
            {
                _log.ExpectionMessage($"Could not create property {propertyName}", ex);
            }
            return bResult;
        }

        public void Export(string fileSuffix)
        {
            try
            {
                var fileName = _xmlFolderPath + _driverObject.Name + "_" + fileSuffix + ".xml";
                bool retCode = _driverObject.ExportToXml(fileName);
                _log.Message(retCode == false
                    ? $" Could not export configuration for driver [{_driverObject.Identification}]"
                    : $" Exported configuration for driver [{_driverObject.Identification}]");
            }
            catch (Exception ex)
            {
                _log.Message($"exception [{ex}]");
            }
        }

        public void Import(string fileSuffix)
        {
            string fileName = _xmlFolderPath + _driverObject.Name + "_" + fileSuffix + ".xml";

            bool retCode = _driverObject.ImportFromXml(fileName);
            _log.Message(retCode == false
                ? $" Could not import configuration for driver [{_driverObject.Identification}]"
                : $" Imported configuration for driver [{_driverObject.Identification}]");
        }

        public void ModifyCommonProperties()
        {
            _log.FunctionEntryMessage("modify common options");

            AddToSignedProperty("DrvConfig.GenGlobalUpdateTime", 200);
            AddToSignedProperty("DrvConfig.GenPrioUpdateTime0", 200);
            AddToSignedProperty("DrvConfig.GenPrioUpdateTime1", 200);
            AddToSignedProperty("DrvConfig.GenPrioUpdateTime2", 200);
            AddToSignedProperty("DrvConfig.GenPrioUpdateTime3", 200);
            SetBooleanProperty("DrvConfig.GenUseGlobalUpdateTime");
            SetBooleanProperty("DrvConfig.GenKeepUpdateList");
            SetBooleanProperty("DrvConfig.GenOutputWriteable");
            SetBooleanProperty("DrvConfig.GenRemanentImage");
            SetBooleanProperty("DrvConfig.GenStopPassiveDrv");
            IncreaseUnsignedProperty("DrvConfig.GenDriverMode", 0, 3);

            _log.FunctionExitMessage();
        }

        public void ModifyCOMProperties()
        {
            _log.FunctionEntryMessage("modify COM properties");

            SetStringProperty("DrvConfig.Com.Device", "COM99", true);
            IncreaseUnsignedProperty("DrvConfig.Com.PortID", 1, 99);
            SetUnsignedProperty("DrvConfig.Com.BaudRate", 38400, 1, 2000000, true);
            IncreaseUnsignedProperty("DrvConfig.Com.ByteSize", 7, 8);
            IncreaseUnsignedProperty("DrvConfig.Com.Parity", 0, 4);
            IncreaseUnsignedProperty("DrvConfig.Com.StopBits", 0, 2);
            IncreaseUnsignedProperty("DrvConfig.Com.Protocol", 1, 4);
            SetStringProperty("DrvConfig.Com.PhoneNumber", "+43 662 488477", true);
            IncreaseUnsignedProperty("DrvConfig.Com.RxIdleTime", 1, 9999);
            IncreaseUnsignedProperty("DrvConfig.Com.NetAddress", 0, 999);
            IncreaseUnsignedProperty("DrvConfig.Com.ReCallIdleTime", 1, 999999);
            IncreaseUnsignedProperty("DrvConfig.Com.ConnectTime", 1, 999999);
            SetBooleanProperty("DrvConfig.Com.Modem");
            SetBooleanProperty("DrvConfig.Com.AutoConnect");
            SetBooleanProperty("DrvConfig.Com.Callback");

            _log.FunctionExitMessage();
        }

        public void SetStringProperty(string propertyName, string setValue, bool modEmpty)
        {
            try
            {
                var propValue = _driverObject.GetDynamicProperty(propertyName);
                string propValueOrg = Convert.ToString(propValue);
                if (modEmpty || propValueOrg.Length > 0)
                {
                    string propValueNew = setValue;
                    _driverObject.SetDynamicProperty(propertyName, propValueNew);
                    _log.PropertyModifiedMessage(propertyName, propValueOrg, propValueNew, propValue.GetType().Name);
                }
                else
                {
                    _log.InvalidPropertyValueMessage(propertyName);
                }
            }
            catch (Exception ex)
            {
                _log.ExpectionMessage($"Could not modify string property {propertyName}", ex);
            }
        }

        public void SetCharacterProperty(string propertyName, char setValue)
        {
            try
            {
                var propValue = _driverObject.GetDynamicProperty(propertyName);
                char propValueOrg = Convert.ToChar(propValue);
                char propValueNew = setValue;
                _driverObject.SetDynamicProperty(propertyName, propValueNew);
                _log.PropertyModifiedMessage(propertyName, propValueOrg, propValueNew, propValue.GetType().Name);
            }
            catch (Exception ex)
            {
                _log.ExpectionMessage($"Could not modify string property {propertyName}", ex);
            }
        }

        public void SetUnsignedProperty(string propertyName, uint setValue, uint minimum, uint maximum, bool modInvalid)
        {
            try
            {
                var propValue = _driverObject.GetDynamicProperty(propertyName);
                uint propValueOrg = Convert.ToUInt32(propValue);
                if (modInvalid || propValueOrg >= minimum && propValueOrg <= maximum)
                {
                    uint propValueNew = setValue;
                    _driverObject.SetDynamicProperty(propertyName, propValueNew);
                    _log.PropertyModifiedMessage(propertyName, propValueOrg, propValueNew, propValue.GetType().Name);
                }
                else
                {
                    _log.InvalidPropertyValueMessage(propertyName);
                }
            }
            catch (Exception ex)
            {
                _log.ExpectionMessage($"Could not modify unsigned property {propertyName}", ex);
            }
        }
        public void SetUnsignedProperty(string propertyName, uint first, uint second)
        {
            try
            {
                var propValue = _driverObject.GetDynamicProperty(propertyName);
                uint propValueOrg = Convert.ToUInt32(propValue);
                var propValueNew = propValueOrg != first ? first : second;
                _driverObject.SetDynamicProperty(propertyName, propValueNew);
                _log.PropertyModifiedMessage(propertyName, propValueOrg, propValueNew, propValue.GetType().Name);
            }
            catch (Exception ex)
            {
                _log.ExpectionMessage($"Could not modify unsigned property {propertyName}", ex);
            }
        }
        public void IncreaseUnsignedProperty(string propertyName, UInt64 minimum, UInt64 maximum)
        {
            try
            {
                var propValue = _driverObject.GetDynamicProperty(propertyName);
                UInt64 propValueOrg = Convert.ToUInt64(propValue);
                UInt64 propValueNew = propValueOrg + 1;
                if (propValueNew > maximum)
                {
                    propValueNew = minimum;
                }
                _driverObject.SetDynamicProperty(propertyName, propValueNew);
                _log.PropertyModifiedMessage(propertyName, propValueOrg, propValueNew, propValue.GetType().Name);
            }
            catch (Exception ex)
            {
                _log.ExpectionMessage($"Could not modify unsigned property {propertyName}", ex);
            }
        }

        public void IncreaseDoubleProperty(string propertyName, double minimum, double maximum)
        {
            try
            {
                var propValue = _driverObject.GetDynamicProperty(propertyName);
                double propValueOrg = Convert.ToDouble(propValue);
                double propValueNew = propValueOrg + 1.0;
                if (propValueNew > maximum)
                {
                    propValueNew = minimum;
                }
                _driverObject.SetDynamicProperty(propertyName, propValueNew);
                _log.PropertyModifiedMessage(propertyName, propValueOrg, propValueNew, propValue.GetType().Name);
            }
            catch (Exception ex)
            {
                _log.ExpectionMessage($"Could not modify double property {propertyName}", ex);
            }
        }

        public void SetSignedProperty(string propertyName, int setValue, int minimum, int maximum, bool modInvalid)
        {
            try
            {
                var propValue = _driverObject.GetDynamicProperty(propertyName);
                int propValueOrg = Convert.ToInt32(propValue);
                if (modInvalid || propValueOrg >= minimum && propValueOrg <= maximum)
                {
                    int propValueNew = setValue;
                    _driverObject.SetDynamicProperty(propertyName, propValueNew);
                    _log.PropertyModifiedMessage(propertyName, propValueOrg, propValueNew, propValue.GetType().Name);
                }
                else
                {
                    _log.InvalidPropertyValueMessage(propertyName);
                }
            }
            catch (Exception ex)
            {
                _log.ExpectionMessage($"Could not modify signed property {propertyName}", ex);
            }
        }
        public void IncreaseSignedProperty(string propertyName, Int64 minimum, Int64 maximum)
        {
            try
            {
                var propValue = _driverObject.GetDynamicProperty(propertyName);
                Int64 propValueOrg = Convert.ToInt64(propValue);
                Int64 propValueNew = propValueOrg + 1;
                if (propValueNew > maximum)
                {
                    propValueNew = minimum;
                }
                _driverObject.SetDynamicProperty(propertyName, propValueNew);
                _log.PropertyModifiedMessage(propertyName, propValueOrg, propValueNew, propValue.GetType().Name);
            }
            catch (Exception ex)
            {
                _log.ExpectionMessage($"Could not modify signed property {propertyName}", ex);
            }
        }
        public void AddToSignedProperty(string propertyName, int addValue)
        {
            try
            {
                var propValue = _driverObject.GetDynamicProperty(propertyName);
                int propValueOrg = Convert.ToInt32(propValue);
                int propValueNew = propValueOrg + addValue;
                _driverObject.SetDynamicProperty(propertyName, propValueNew);
                _log.PropertyModifiedMessage(propertyName, propValueOrg, propValueNew, propValue.GetType().Name);
            }
            catch (Exception ex)
            {
                _log.ExpectionMessage($"Could not modify signed property {propertyName}", ex);
            }
        }

        public void SetBooleanProperty(string propName)
        {
            try
            {
                var propValue = _driverObject.GetDynamicProperty(propName);
                bool propValueOrg = Convert.ToBoolean(propValue);
                bool propValueNew = !propValueOrg;
                _driverObject.SetDynamicProperty(propName, propValueNew);
                _log.PropertyModifiedMessage(propName, propValueOrg, propValueNew, propValue.GetType().Name);
            }
            catch (Exception ex)
            {
                _log.ExpectionMessage($"Could not modify boolean property {propName}", ex);
            }
        }

    }
}
