using System;
using System.IO;
using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.Variable;

namespace Common
{
  class Test
  {

    private static IEditorApplication m_cContext;
    private static IDriver m_cDrvObject;

    private static string m_sDriverIdent;
    private static string m_sDriverName;
    private static string m_sDriverAPIName;
    private static string m_sXmlFolder;

    public static void PrepareEnvironment(IEditorApplication cContext,
      string s_sDriverIdent, string sDriverName, bool useTempFolder)
    {
      m_cContext = cContext;

      m_sDriverIdent = s_sDriverIdent;
      m_sDriverName = sDriverName;
      m_sDriverAPIName = s_sDriverIdent + "API";
      if (useTempFolder)
      {
        m_sXmlFolder = Environment.GetEnvironmentVariable("TEMP") + "\\TestDriverAPI\\";
      }
      else
      {
        m_sXmlFolder = Path.GetFullPath(".\\XMLData\\");
      }
      
        //m_sXmlFolder = ".\\..\\..\\XMLData\\";

      IWorkspace iWorkSpace = m_cContext.Workspace;
      IProject iProject = iWorkSpace.ActiveProject;
      IDriverCollection iDrivers = iProject.DriverCollection;
      m_cDrvObject = iDrivers[sDriverName];
    }

    public static void Message(string msgText)
    {
      m_cContext.DebugPrint($" - [{m_sDriverAPIName}]: {msgText}", DebugPrintStyle.Standard);
    }
    public static void MessageInvalidRemains(string propName)
    {
      m_cContext.DebugPrint($" - [{m_sDriverAPIName}]:     [{propName}] is invalid (and remains invalid)", DebugPrintStyle.Standard);
    }
    public static void MessageException(string msgText, Exception ex)
    {
      m_cContext.DebugPrint($" - [{m_sDriverAPIName}]:     [{msgText}] (Exception: {ex.Message})", DebugPrintStyle.Standard);
    }
    public static void MessageFncEntry(string msgText)
    {
      m_cContext.DebugPrint($" - [{m_sDriverAPIName}]:   {msgText}", DebugPrintStyle.Standard);
    }
    public static void MessageFncExit()
    {
    }
    public static void MessageModified(string propName, object orgValue, object newValue, string propType)
    {
      m_cContext.DebugPrint($" - [{m_sDriverAPIName}]:    [{propName}] from [{orgValue}] to [{newValue}] (value type: {propType})", DebugPrintStyle.Standard);
    }

    public static bool OpenDriver(int iTimeoutInMS)
    {
      bool bFirst = true;
      DateTime StartTime = DateTime.Now;
      DateTime EndTime = DateTime.Now;
      EndTime.AddMilliseconds(iTimeoutInMS);

      do
      {
        if (m_cDrvObject.InitializeConfiguration() == true)
        {
          Message($" Opened driver[{ m_cDrvObject.Identification}]");
          return true;
        }
        else if (bFirst == true)
        {
          bFirst = false;
          if (m_cDrvObject.EndConfiguration(true))
          {
            Message($" Closed driver [{m_cDrvObject.Identification}] - was previously open");
          }
        }
        System.Threading.Thread.Sleep(100);
      } while (StartTime < EndTime);

      Message($" Could not open driver [{m_cDrvObject.Identification}]");
      return false;
    }

    public static void CloseDriver()
    {
      m_cDrvObject.EndConfiguration(true);
      Message($" Closed driver [{m_cDrvObject.Identification}]");
    }

    public static void GetNodeInfo(string propName, out string[] cItems, out uint instCount)
    {
      cItems = m_cDrvObject.GetDynamicProperties(propName);
      instCount = Convert.ToUInt32(m_cDrvObject.GetDynamicProperty(propName));
    }

    public static void Export(String fileSuffix)
    {
      try
      {
        String fileName;
        fileName = m_sXmlFolder + m_cDrvObject.Name + "_" + fileSuffix + ".xml";
        bool retCode = m_cDrvObject.ExportToXml(fileName);
        if (retCode == false)
        {
          Message($" Could not export configuration for driver [{m_cDrvObject.Identification}]");
        }
        else
        {
          Message($" Exported configuration for driver [{m_cDrvObject.Identification}]");
        }
      }
      catch (Exception ex)
      {
        Message($"exception [{ex.ToString()}]");
      }
    }

    public static void Import(String fileSuffix)
    {
      String fileName = m_sXmlFolder + m_cDrvObject.Name + "_" + fileSuffix + ".xml";

      bool retCode = m_cDrvObject.ImportFromXml(fileName);
      if (retCode == false)
      {
        Message($" Could not import configuration for driver [{m_cDrvObject.Identification}]");
      }
      else
      {
        Message($" Imported configuration for driver [{m_cDrvObject.Identification}]");
      }
    }

    public static void ModifyCommonProperties()
    {
      MessageFncEntry("modify common options");

      ModifySignedProperty_Add("DrvConfig.GenGlobalUpdateTime", 200);
      ModifySignedProperty_Add("DrvConfig.GenPrioUpdateTime0", 200);
      ModifySignedProperty_Add("DrvConfig.GenPrioUpdateTime1", 200);
      ModifySignedProperty_Add("DrvConfig.GenPrioUpdateTime2", 200);
      ModifySignedProperty_Add("DrvConfig.GenPrioUpdateTime3", 200);
      ModifyBooleanProperty("DrvConfig.GenUseGlobalUpdateTime");
      ModifyBooleanProperty("DrvConfig.GenKeepUpdateList");
      ModifyBooleanProperty("DrvConfig.GenOutputWriteable");
      ModifyBooleanProperty("DrvConfig.GenRemanentImage");
      ModifyBooleanProperty("DrvConfig.GenStopPassiveDrv");
      ModifyUnsignedProperty_Inc("DrvConfig.GenDriverMode", 0, 3);

      MessageFncExit();
    }

    public static void ModifyCOMProperties()
    {
      MessageFncEntry("modify COM properties");

      ModifyStringProperty_Set("DrvConfig.Com.Device", "COM99", true);
      ModifyUnsignedProperty_Inc("DrvConfig.Com.PortID", 1, 99);
      ModifyUnsignedProperty_Set("DrvConfig.Com.BaudRate", 38400, 1, 2000000, true);
      ModifyUnsignedProperty_Inc("DrvConfig.Com.ByteSize", 7, 8);
      ModifyUnsignedProperty_Inc("DrvConfig.Com.Parity", 0, 4);
      ModifyUnsignedProperty_Inc("DrvConfig.Com.StopBits", 0, 2);
      ModifyUnsignedProperty_Inc("DrvConfig.Com.Protocol", 1, 4);
      ModifyStringProperty_Set("DrvConfig.Com.PhoneNumber", "+43 662 488477", true);
      ModifyUnsignedProperty_Inc("DrvConfig.Com.RxIdleTime", 1, 9999);
      ModifyUnsignedProperty_Inc("DrvConfig.Com.NetAddress", 0, 999);
      ModifyUnsignedProperty_Inc("DrvConfig.Com.ReCallIdleTime", 1, 999999);
      ModifyUnsignedProperty_Inc("DrvConfig.Com.ConnectTime", 1, 999999);
      ModifyBooleanProperty("DrvConfig.Com.Modem");
      ModifyBooleanProperty("DrvConfig.Com.AutoConnect");
      ModifyBooleanProperty("DrvConfig.Com.Callback");

      MessageFncExit();
    }

    public static void ModifyStringProperty_Set(string propName, string setValue, bool modEmpty)
    {
      try
      {
        var propValue = m_cDrvObject.GetDynamicProperty(propName);
        string propValueOrg = Convert.ToString(propValue);
        if (modEmpty == true || propValueOrg.Length > 0)
        {
          string propValueNew = setValue;
          m_cDrvObject.SetDynamicProperty(propName, propValueNew);
          MessageModified(propName, propValueOrg, propValueNew, propValue.GetType().Name);
        }
        else
        {
          MessageInvalidRemains(propName);
        }
      }
      catch(Exception ex)
      {
        MessageException($"Could not modify string property [propName]", ex);
      }
    }

    public static void ModifyUnsignedProperty_Set(string propName, UInt32 uiSet, UInt32 uiMin, UInt32 uiMax, bool modInvalid)
    {
      try
      {
        var propValue = m_cDrvObject.GetDynamicProperty(propName);
        UInt32 propValueOrg = Convert.ToUInt32(propValue);
        if (modInvalid == true || (propValueOrg >= uiMin && propValueOrg <= uiMax))
        {
          UInt32 propValueNew = uiSet;
          m_cDrvObject.SetDynamicProperty(propName, propValueNew);
          MessageModified(propName, propValueOrg, propValueNew, propValue.GetType().Name);
        }
        else
        {
          MessageInvalidRemains(propName);
        }
      }
      catch (Exception ex)
      {
        MessageException($"Could not modify unsigned property [propName]", ex);
      }
    }
    public static void ModifyUnsignedProperty_Sel(string propName, UInt32 uiFirst, UInt32 uiSecond)
    {
      try
      {
        var propValue = m_cDrvObject.GetDynamicProperty(propName);
        UInt32 propValueOrg = Convert.ToUInt32(propValue);
        uint propValueNew;
        if (propValueOrg != uiFirst)
        {
          propValueNew = uiFirst;
        }
        else
        {
          propValueNew = uiSecond;
        }
        m_cDrvObject.SetDynamicProperty(propName, propValueNew);
        MessageModified(propName, propValueOrg, propValueNew, propValue.GetType().Name);
      }
      catch (Exception ex)
      {
        MessageException($"Could not modify unsigned property [propName]", ex);
      }
    }
    public static void ModifyUnsignedProperty_Inc(string propName, UInt32 uiMin, UInt32 uiMax)
    {
      try
      {
        var propValue = m_cDrvObject.GetDynamicProperty(propName);
        UInt32 propValueOrg = Convert.ToUInt32(propValue);
        uint propValueNew = propValueOrg + 1;
        if (propValueNew > uiMax)
        {
          propValueNew = uiMin;
        }
        m_cDrvObject.SetDynamicProperty(propName, propValueNew);
        MessageModified(propName, propValueOrg, propValueNew, propValue.GetType().Name);
      }
      catch (Exception ex)
      {
        MessageException($"Could not modify unsigned property [propName]", ex);
      }
    }

    public static void ModifyDoubleProperty_Inc(string propName, Double dblMin, Double dblMax)
    {
      try
      {
        var propValue = m_cDrvObject.GetDynamicProperty(propName);
        Double propValueOrg = Convert.ToDouble(propValue);
        Double propValueNew = propValueOrg + 1.0;
        if (propValueNew > dblMax)
        {
          propValueNew = dblMin;
        }
        m_cDrvObject.SetDynamicProperty(propName, propValueNew);
        MessageModified(propName, propValueOrg, propValueNew, propValue.GetType().Name);
      }
      catch (Exception ex)
      {
        MessageException($"Could not modify double property [propName]", ex);
      }
    }

    public static void ModifySignedProperty_Set(string propName, Int32 iSet, Int32 iMin, Int32 iMax, bool modInvalid)
    {
      try
      {
        var propValue = m_cDrvObject.GetDynamicProperty(propName);
        Int32 propValueOrg = Convert.ToInt32(propValue);
        if (modInvalid == true || (propValueOrg >= iMin && propValueOrg <= iMax))
        {
          Int32 propValueNew = iSet;
          m_cDrvObject.SetDynamicProperty(propName, propValueNew);
          MessageModified(propName, propValueOrg, propValueNew, propValue.GetType().Name);
        }
        else
        {
          MessageInvalidRemains(propName);
        }
      }
      catch (Exception ex)
      {
        MessageException($"Could not modify signed property [propName]", ex);
      }
    }
    public static void ModifySignedProperty_Inc(string propName, Int32 uiMin, Int32 uiMax)
    {
      try
      {
        var propValue = m_cDrvObject.GetDynamicProperty(propName);
        Int32 propValueOrg = Convert.ToInt32(propValue);
        Int32 propValueNew = propValueOrg + 1;
        if (propValueNew > uiMax)
        {
          propValueNew = uiMin;
        }
        m_cDrvObject.SetDynamicProperty(propName, propValueNew);
        MessageModified(propName, propValueOrg, propValueNew, propValue.GetType().Name);
      }
      catch (Exception ex)
      {
        MessageException($"Could not modify signed property [propName]", ex);
      }
    }
    public static void ModifySignedProperty_Add(string propName, Int32 addValue)
    {
      try
      {
        var propValue = m_cDrvObject.GetDynamicProperty(propName);
        Int32 propValueOrg = Convert.ToInt32(propValue);
        Int32 propValueNew = propValueOrg + addValue;
        m_cDrvObject.SetDynamicProperty(propName, propValueNew);
        MessageModified(propName, propValueOrg, propValueNew, propValue.GetType().Name);
      }
      catch (Exception ex)
      {
        MessageException($"Could not modify signed property [propName]", ex);
      }
    }

    public static void ModifyBooleanProperty(string propName)
    {
      try
      {
        var propValue = m_cDrvObject.GetDynamicProperty(propName);
        bool propValueOrg = Convert.ToBoolean(propValue);
        bool propValueNew = !propValueOrg;
        m_cDrvObject.SetDynamicProperty(propName, propValueNew);
        MessageModified(propName, propValueOrg, propValueNew, propValue.GetType().Name);
      }
      catch (Exception ex)
      {
        MessageException($"Could not modify boolean property [propName]", ex);
      }
    }

  }
}
