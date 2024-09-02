//AUTOGENERATED FILE. Do not make any manual changes. Any changes to this file will be overwritten.

using Scada.AddIn.Contracts.Function;

namespace zenonExtensions
{
  public static class ScriptExtension
  {
/// Sets Name
    public static void SetName(this IScript script, string value)
    {
      script.SetDynamicProperty("Name", value);
    }

/// Gets Name
    public static string GetName(this IScript script)
    {
      return (string) script.GetDynamicProperty("Name");
    }

/// Sets Smart Object
    public static void SetSOSourceName(this IScript script, string value)
    {
      script.SetDynamicProperty("SOSourceName", value);
    }

/// Gets Smart Object
    public static string GetSOSourceName(this IScript script)
    {
      return (string) script.GetDynamicProperty("SOSourceName");
    }

/// Sets Equipment Groups
    public static void SetSystemModelGroup(this IScript script, object value)
    {
      script.SetDynamicProperty("SystemModelGroup", value);
    }

/// Gets Equipment Groups
    public static object GetSystemModelGroup(this IScript script)
    {
      return (object) script.GetDynamicProperty("SystemModelGroup");
    }

  }
}