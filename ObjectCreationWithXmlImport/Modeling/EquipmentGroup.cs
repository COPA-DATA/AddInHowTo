using System;
using System.Collections.Generic;
using System.Linq;

namespace XmlImporter.EquipmentModeling
{
  public class EquipmentGroup
  {
    public string Name { get; set; }
    public string InternalGuid { get; set; }
    public List<EquipmentGroup> EquipmentGroups { get; set; }

    public EquipmentGroup(string name)
    {
      Name = name;
      InternalGuid = Guid.NewGuid().ToString();
      EquipmentGroups = new List<EquipmentGroup>();
    }

    public EquipmentGroup(string name, params EquipmentGroup[] equipmentGroup) :this(name)
    {
      EquipmentGroups.AddRange(equipmentGroup.ToList());
    }
  }
}
