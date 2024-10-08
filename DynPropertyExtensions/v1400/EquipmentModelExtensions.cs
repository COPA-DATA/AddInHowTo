//AUTOGENERATED FILE. Do not make any manual changes. Any changes to this file will be overwritten.

using Scada.AddIn.Contracts.EquipmentModeling;

namespace zenonExtensions
{
  public static class EquipmentModelExtension
  {
/// Sets Name
    public static void SetName(this IEquipmentModel systemModel, string value)
    {
      systemModel.SetDynamicProperty("Name", value);
    }

/// Gets Name
    public static string GetName(this IEquipmentModel systemModel)
    {
      return (string) systemModel.GetDynamicProperty("Name");
    }

/// Sets Equipment Groups
    public static void SetSystemModelGroup(this IEquipmentModel systemModel, object value)
    {
      systemModel.SetDynamicProperty("SystemModelGroup", value);
    }

/// Gets Equipment Groups
    public static object GetSystemModelGroup(this IEquipmentModel systemModel)
    {
      return (object) systemModel.GetDynamicProperty("SystemModelGroup");
    }

/// Sets Guid
    public static void SetGuid(this IEquipmentModel systemModel, string value)
    {
      systemModel.SetDynamicProperty("Guid", value);
    }

/// Gets Guid
    public static string GetGuid(this IEquipmentModel systemModel)
    {
      return (string) systemModel.GetDynamicProperty("Guid");
    }

/// Sets Equipment model relevant for operating authorization
    public static void SetRelevantForNetToken(this IEquipmentModel systemModel, bool value)
    {
      systemModel.SetDynamicProperty("RelevantForNetToken", value);
    }

/// Gets Equipment model relevant for operating authorization
    public static bool GetRelevantForNetToken(this IEquipmentModel systemModel)
    {
      return (bool) systemModel.GetDynamicProperty("RelevantForNetToken");
    }

/// Sets Model Type
    public static void SetModelType(this IEquipmentModel systemModel, short value)
    {
      systemModel.SetDynamicProperty("ModelType", value);
    }

/// Gets Model Type
    public static short GetModelType(this IEquipmentModel systemModel)
    {
      return (short) systemModel.GetDynamicProperty("ModelType");
    }

  }
}