using System;
using System.Collections.Generic;
using System.Linq;

namespace XmlImporter.EquipmentModeling
{
  public class EquipmentModel : IModel
  {
    public string Name { get; set; }
    public string InternalGuid { get; set; }
    public List<EquipmentGroup> EquipmentGroups { get; set; }

    public EquipmentModel(string name)
    {
      Name = name;
      InternalGuid = Guid.NewGuid().ToString();
      EquipmentGroups = new List<EquipmentGroup>();
    }
    public EquipmentModel(string name, params EquipmentGroup[] equipmentGroup) : this(name)
    {
      EquipmentGroups.AddRange(equipmentGroup.ToList());
    }
  }
}
