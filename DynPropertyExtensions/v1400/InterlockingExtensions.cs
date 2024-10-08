//AUTOGENERATED FILE. Do not make any manual changes. Any changes to this file will be overwritten.

using Scada.AddIn.Contracts.Interlocking;

namespace zenonExtensions
{
  public static class InterlockingExtension
  {
/// Sets Watchdog timer
    public static void SetWatchDogTimer(this IInterlocking interlocking, byte value)
    {
      interlocking.SetDynamicProperty("WatchDogTimer", value);
    }

/// Gets Watchdog timer
    public static byte GetWatchDogTimer(this IInterlocking interlocking)
    {
      return (byte) interlocking.GetDynamicProperty("WatchDogTimer");
    }

/// Sets Set status PROGRESS
    public static void SetDesiredDirection(this IInterlocking interlocking, bool value)
    {
      interlocking.SetDynamicProperty("DesiredDirection", value);
    }

/// Gets Set status PROGRESS
    public static bool GetDesiredDirection(this IInterlocking interlocking)
    {
      return (bool) interlocking.GetDynamicProperty("DesiredDirection");
    }

/// Sets Exp. menu spec. screen
    public static void SetPicture(this IInterlocking interlocking, string value)
    {
      interlocking.SetDynamicProperty("Picture", value);
    }

/// Gets Exp. menu spec. screen
    public static string GetPicture(this IInterlocking interlocking)
    {
      return (string) interlocking.GetDynamicProperty("Picture");
    }

/// Sets Replace in screen
    public static void SetSubstiGroup(this IInterlocking interlocking, bool value)
    {
      interlocking.SetDynamicProperty("SubstiGroup", value);
    }

/// Gets Replace in screen
    public static bool GetSubstiGroup(this IInterlocking interlocking)
    {
      return (bool) interlocking.GetDynamicProperty("SubstiGroup");
    }

/// Sets Screen modal
    public static void SetModal(this IInterlocking interlocking, bool value)
    {
      interlocking.SetDynamicProperty("Modal", value);
    }

/// Gets Screen modal
    public static bool GetModal(this IInterlocking interlocking)
    {
      return (bool) interlocking.GetDynamicProperty("Modal");
    }

/// Sets Screen title from response variable
    public static void SetRM_Titel(this IInterlocking interlocking, bool value)
    {
      interlocking.SetDynamicProperty("RM_Titel", value);
    }

/// Gets Screen title from response variable
    public static bool GetRM_Titel(this IInterlocking interlocking)
    {
      return (bool) interlocking.GetDynamicProperty("RM_Titel");
    }

/// Sets List configuration export
    public static void SetListConfigEXP(this IInterlocking interlocking, object value)
    {
      interlocking.SetDynamicProperty("ListConfigEXP", value);
    }

/// Gets List configuration export
    public static object GetListConfigEXP(this IInterlocking interlocking)
    {
      return (object) interlocking.GetDynamicProperty("ListConfigEXP");
    }

/// Sets Breaker tripping detection
    public static void SetCBTR_Act(this IInterlocking interlocking, bool value)
    {
      interlocking.SetDynamicProperty("CBTR_Act", value);
    }

/// Gets Breaker tripping detection
    public static bool GetCBTR_Act(this IInterlocking interlocking)
    {
      return (bool) interlocking.GetDynamicProperty("CBTR_Act");
    }

/// Sets Suppress detection
    public static void SetCBTR_Formula(this IInterlocking interlocking, string value)
    {
      interlocking.SetDynamicProperty("CBTR_Formula", value);
    }

/// Gets Suppress detection
    public static string GetCBTR_Formula(this IInterlocking interlocking)
    {
      return (string) interlocking.GetDynamicProperty("CBTR_Formula");
    }

/// Sets Write status bits to command variables
    public static void SetWriteStatusBitActive(this IInterlocking interlocking, bool value)
    {
      interlocking.SetDynamicProperty("WriteStatusBitActive", value);
    }

/// Gets Write status bits to command variables
    public static bool GetWriteStatusBitActive(this IInterlocking interlocking)
    {
      return (bool) interlocking.GetDynamicProperty("WriteStatusBitActive");
    }

  }
}