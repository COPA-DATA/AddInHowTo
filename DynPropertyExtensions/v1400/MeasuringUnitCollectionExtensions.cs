//AUTOGENERATED FILE. Do not make any manual changes. Any changes to this file will be overwritten.

using Scada.AddIn.Contracts.UnitConversion;

namespace zenonExtensions
{
  public static class MeasuringUnitCollectionExtension
  {
/// Sets Measuring unit
    public static void SetName(this IMeasuringUnitCollection units, string value)
    {
      units.SetDynamicProperty("Name", value);
    }

/// Gets Measuring unit
    public static string GetName(this IMeasuringUnitCollection units)
    {
      return (string) units.GetDynamicProperty("Name");
    }

/// Sets Equipment Groups
    public static void SetSystemModelGroup(this IMeasuringUnitCollection units, object value)
    {
      units.SetDynamicProperty("SystemModelGroup", value);
    }

/// Gets Equipment Groups
    public static object GetSystemModelGroup(this IMeasuringUnitCollection units)
    {
      return (object) units.GetDynamicProperty("SystemModelGroup");
    }

/// Sets Factor
    public static void SetFactor(this IMeasuringUnitCollection units, double value)
    {
      units.SetDynamicProperty("Factor", value);
    }

/// Gets Factor
    public static double GetFactor(this IMeasuringUnitCollection units)
    {
      return (double) units.GetDynamicProperty("Factor");
    }

/// Sets Offset
    public static void SetOffset(this IMeasuringUnitCollection units, double value)
    {
      units.SetDynamicProperty("Offset", value);
    }

/// Gets Offset
    public static double GetOffset(this IMeasuringUnitCollection units)
    {
      return (double) units.GetDynamicProperty("Offset");
    }

/// Sets Shift of the decimal point
    public static void SetDecShift(this IMeasuringUnitCollection units, short value)
    {
      units.SetDynamicProperty("DecShift", value);
    }

/// Gets Shift of the decimal point
    public static short GetDecShift(this IMeasuringUnitCollection units)
    {
      return (short) units.GetDynamicProperty("DecShift");
    }

  }
}