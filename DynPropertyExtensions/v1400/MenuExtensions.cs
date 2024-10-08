//AUTOGENERATED FILE. Do not make any manual changes. Any changes to this file will be overwritten.

using Scada.AddIn.Contracts.Menu;

namespace zenonExtensions
{
  public static class MenuExtension
  {
/// Sets Pulldown
    public static void SetPulldown(this IMenu zenMenu, bool value)
    {
      zenMenu.SetDynamicProperty("Pulldown", value);
    }

/// Gets Pulldown
    public static bool GetPulldown(this IMenu zenMenu)
    {
      return (bool) zenMenu.GetDynamicProperty("Pulldown");
    }

/// Sets Notes
    public static void SetNotes(this IMenu zenMenu, string value)
    {
      zenMenu.SetDynamicProperty("Notes", value);
    }

/// Gets Notes
    public static string GetNotes(this IMenu zenMenu)
    {
      return (string) zenMenu.GetDynamicProperty("Notes");
    }

/// Sets Name
    public static void SetName(this IMenu zenMenu, string value)
    {
      zenMenu.SetDynamicProperty("Name", value);
    }

/// Gets Name
    public static string GetName(this IMenu zenMenu)
    {
      return (string) zenMenu.GetDynamicProperty("Name");
    }

/// Sets Font
    public static void SetFont(this IMenu zenMenu, ushort value)
    {
      zenMenu.SetDynamicProperty("Font", value);
    }

/// Gets Font
    public static ushort GetFont(this IMenu zenMenu)
    {
      return (ushort) zenMenu.GetDynamicProperty("Font");
    }

/// Sets Text color
    public static void SetTextColor(this IMenu zenMenu, uint value)
    {
      zenMenu.SetDynamicProperty("TextColor", value);
    }

/// Gets Text color
    public static uint GetTextColor(this IMenu zenMenu)
    {
      return (uint) zenMenu.GetDynamicProperty("TextColor");
    }

/// Sets Background color
    public static void SetBackColor(this IMenu zenMenu, uint value)
    {
      zenMenu.SetDynamicProperty("BackColor", value);
    }

/// Gets Background color
    public static uint GetBackColor(this IMenu zenMenu)
    {
      return (uint) zenMenu.GetDynamicProperty("BackColor");
    }

/// Sets Equipment Groups
    public static void SetSystemModelGroup(this IMenu zenMenu, object value)
    {
      zenMenu.SetDynamicProperty("SystemModelGroup", value);
    }

/// Gets Equipment Groups
    public static object GetSystemModelGroup(this IMenu zenMenu)
    {
      return (object) zenMenu.GetDynamicProperty("SystemModelGroup");
    }

  }
}