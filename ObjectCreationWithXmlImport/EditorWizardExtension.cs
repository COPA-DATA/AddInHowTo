using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using Scada.AddIn.Contracts;
using XmlImporter.Creators;
using XmlImporter.EquipmentModeling;

namespace XmlImporter
{
  /// <summary>
  /// Description of Editor Wizard Extension.
  /// </summary>
  [AddInExtension(name: "XmlImporter",
    description: "This Editor Wizard demonstrates how to create an Equipment model and some variables by the means of the XML Import",
    category: "Training")]
  public class EditorWizardExtension : IEditorWizardExtension
  {
    #region IEditorWizardExtension implementation

    public void Run(IEditorApplication context, IBehavior behavior)
    {
      var equipmentStructure = new EquipmentModeling.EquipmentModel("MyEquipment",
        new EquipmentGroup("Group_1",
          new EquipmentGroup("Sub 1"),
          new EquipmentGroup("Sub 2")),
        new EquipmentGroup("Group_2",
          new EquipmentGroup("Sub_3",
           new EquipmentGroup("Sub_4",
             new EquipmentGroup("Sub_5")))));

      try
      {
        var creator = new EquipmentCreator();
        creator.Create(context, equipmentStructure);
      }
      catch (Exception ex)
      {
        context.DebugPrint("ERROR: Exception: " + ex.Message, DebugPrintStyle.Error);
        throw;
      }




    }

    #endregion
  }

}