//AUTOGENERATED FILE. Do not make any manual changes. Any changes to this file will be overwritten.

using Scada.AddIn.Contracts.Variable;

namespace zenonExtensions
{
  public static class StructureElementExtension
  {
/// Sets TypeID
    public static void SetTypeID(this IStructureElement complexItem, uint value)
    {
      complexItem.SetDynamicProperty("TypeID", value);
    }

/// Gets TypeID
    public static uint GetTypeID(this IStructureElement complexItem)
    {
      return (uint) complexItem.GetDynamicProperty("TypeID");
    }

/// Sets Name
    public static void SetName(this IStructureElement complexItem, string value)
    {
      complexItem.SetDynamicProperty("Name", value);
    }

/// Gets Name
    public static string GetName(this IStructureElement complexItem)
    {
      return (string) complexItem.GetDynamicProperty("Name");
    }

/// Sets Description
    public static void SetDescription(this IStructureElement complexItem, string value)
    {
      complexItem.SetDynamicProperty("Description", value);
    }

/// Gets Description
    public static string GetDescription(this IStructureElement complexItem)
    {
      return (string) complexItem.GetDynamicProperty("Description");
    }

/// Sets Smart Object
    public static void SetSOSourceName(this IStructureElement complexItem, string value)
    {
      complexItem.SetDynamicProperty("SOSourceName", value);
    }

/// Gets Smart Object
    public static string GetSOSourceName(this IStructureElement complexItem)
    {
      return (string) complexItem.GetDynamicProperty("SOSourceName");
    }

/// Sets External reference
    public static void SetExternalReference(this IStructureElement complexItem, string value)
    {
      complexItem.SetDynamicProperty("ExternalReference", value);
    }

/// Gets External reference
    public static string GetExternalReference(this IStructureElement complexItem)
    {
      return (string) complexItem.GetDynamicProperty("ExternalReference");
    }

/// Sets Offset
    public static void SetOffset(this IStructureElement complexItem, uint value)
    {
      complexItem.SetDynamicProperty("Offset", value);
    }

/// Gets Offset
    public static uint GetOffset(this IStructureElement complexItem)
    {
      return (uint) complexItem.GetDynamicProperty("Offset");
    }

/// Sets Bit offset
    public static void SetBitOffset(this IStructureElement complexItem, ushort value)
    {
      complexItem.SetDynamicProperty("BitOffset", value);
    }

/// Gets Bit offset
    public static ushort GetBitOffset(this IStructureElement complexItem)
    {
      return (ushort) complexItem.GetDynamicProperty("BitOffset");
    }

/// Sets IEC data type
    public static void SetID_DataTyp(this IStructureElement complexItem, uint value)
    {
      complexItem.SetDynamicProperty("ID_DataTyp", value);
    }

/// Gets IEC data type
    public static uint GetID_DataTyp(this IStructureElement complexItem)
    {
      return (uint) complexItem.GetDynamicProperty("ID_DataTyp");
    }

/// Sets Start index
    public static void SetLBound(this IStructureElement complexItem, ushort value)
    {
      complexItem.SetDynamicProperty("LBound", value);
    }

/// Gets Start index
    public static ushort GetLBound(this IStructureElement complexItem)
    {
      return (ushort) complexItem.GetDynamicProperty("LBound");
    }

/// Sets Dim 1
    public static void SetDim1(this IStructureElement complexItem, uint value)
    {
      complexItem.SetDynamicProperty("Dim1", value);
    }

/// Gets Dim 1
    public static uint GetDim1(this IStructureElement complexItem)
    {
      return (uint) complexItem.GetDynamicProperty("Dim1");
    }

/// Sets Dim 2
    public static void SetDim2(this IStructureElement complexItem, uint value)
    {
      complexItem.SetDynamicProperty("Dim2", value);
    }

/// Gets Dim 2
    public static uint GetDim2(this IStructureElement complexItem)
    {
      return (uint) complexItem.GetDynamicProperty("Dim2");
    }

/// Sets Dim 3
    public static void SetDim3(this IStructureElement complexItem, uint value)
    {
      complexItem.SetDynamicProperty("Dim3", value);
    }

/// Gets Dim 3
    public static uint GetDim3(this IStructureElement complexItem)
    {
      return (uint) complexItem.GetDynamicProperty("Dim3");
    }

/// Sets Pos. in structure
    public static void SetItemIndex(this IStructureElement complexItem, uint value)
    {
      complexItem.SetDynamicProperty("ItemIndex", value);
    }

/// Gets Pos. in structure
    public static uint GetItemIndex(this IStructureElement complexItem)
    {
      return (uint) complexItem.GetDynamicProperty("ItemIndex");
    }

/// Sets linked with data type
    public static void SetHasRef(this IStructureElement complexItem, bool value)
    {
      complexItem.SetDynamicProperty("HasRef", value);
    }

/// Gets linked with data type
    public static bool GetHasRef(this IStructureElement complexItem)
    {
      return (bool) complexItem.GetDynamicProperty("HasRef");
    }

  }
}