using Scada.AddIn.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using XmlImporter.EquipmentModeling;

namespace XmlImporter.Creators
{
  class EquipmentCreator : ICreator
  {
    /// <summary>
    /// This creates an equiment model in zenon by parsing an xml template
    /// previously exported from zenon. This template gets modified according to the
    /// given equipment model. The xml gets saved in a temporary file and imported
    /// to the zenon editor.
    /// </summary>
    /// <param name="model">must describe an equipment model</param>
    /// <exception cref="ArgumentException">is thrown if argument is not of type Equipment modelling</exception>
    public void Create(IEditorApplication context, IModel model)
    {
      #region Argument Validation
      if (!(model is EquipmentModel))
      {
        throw new ArgumentException("The argument is not of type 'EquipmentModeling'");
      }
      if (context == null || context.Workspace.ActiveProject == null)
      {
        throw new ArgumentException("There is no active project in the workspace!");
      }
      #endregion
      // Creating and naming the equipment model
      var xmlEquipmentModel = GetEquipmentModel();
      xmlEquipmentModel.Element(Constants.Name).Value = (model as EquipmentModel).Name;
      xmlEquipmentModel.Element(Constants.Guid).Value = (model as EquipmentModel).InternalGuid;

      // setting all first level groups in equipment model
      SetEquipmentGroups(ref xmlEquipmentModel, model);

      // iterating through first level groups and setting child groups recursively
      foreach (var group in (model as EquipmentModel)?.EquipmentGroups)
      {
        SetEquipmentSubGroups(ref xmlEquipmentModel, group.InternalGuid, group.EquipmentGroups);
      }
      // Creating the Equipment Model in a zenon compatible format
      var xmlTemplate = GetEquipmentModelXml();
      xmlTemplate.Root.Element(Constants.Apartment).Add(new XElement(Constants.SystemModel, xmlEquipmentModel));

      // Create a file for import
      var xmlFile = CreateTempFilePath("EquipmentModel.xml");
      File.WriteAllText(xmlFile, xmlTemplate.ToString());

      //import file to Editor
      context.Workspace.ActiveProject.EquipmentModeling.ImportFromXml(xmlFile);

      // Cleanup
      File.Delete(xmlFile);
    }

    /// <summary>
    /// This creates an xml file from the template in resources
    /// </summary>
    /// <returns>The filepath of the temporary file</returns>
    private string CreateXmlFileFromResourcesTemplate(string filename)
    {
      var tempXmlFile = CreateTempFilePath(filename);
      var template = XmlResources.XML_TEMPLATE_EQUIPMENT_MODEL.Replace("\0", string.Empty);
      File.WriteAllText(tempXmlFile, template);
      return tempXmlFile;
    }

    private string CreateTempFilePath(string filename)
    {
      var tempDirectory = Path.Combine(Path.GetTempPath(), this.GetType().ToString());
      var tempXmlFile = Path.Combine(Path.GetTempPath(), this.GetType().ToString(), filename);
      if (!Directory.Exists(tempDirectory))
      {
        Directory.CreateDirectory(tempDirectory);
      }
      return tempXmlFile;
    }

    /// <summary>
    /// This gets an equipment group from template
    /// </summary>
    /// <returns>The XElement for an equipment group</returns>
    private XElement GetEquipmentGroup()
    {
      var xmlModel = GetModelNode();
      return xmlModel.Element(Constants.Groups).Element(Constants.Group);
    }

    /// <summary>
    /// This gets the equipment model from template (without any groups)
    /// </summary>
    /// <returns>The XElement for an equipment model</returns>
    private XElement GetEquipmentModel()
    {
      var xmlModel = GetModelNode();

      //remove all groups from template
      var groups = xmlModel.Elements().Where(x => x.Element(Constants.Group) != null).ToList();
      foreach (var group in groups)
      {
        group.Remove();
      }
      return xmlModel;
    }

    private XElement GetModelNode()
    {
      var template = CreateXmlFileFromResourcesTemplate("temporary.xml");
      var xmlTemplate = XDocument.Load(template);
      File.Delete(template);
      return xmlTemplate.Root.Element(Constants.Apartment).Element(Constants.SystemModel).Element(Constants.Model);
    }

    /// <summary>
    /// This returns an zenon compatible xml template without any
    /// equipment model definition.
    /// </summary>
    private XDocument GetEquipmentModelXml()
    {
      var template = CreateXmlFileFromResourcesTemplate("temporarytemplate.xml");
      var xmlTemplate = XDocument.Load(template);
      File.Delete(template);
      var systemModel = xmlTemplate.Root.Element(Constants.Apartment).Element(Constants.SystemModel);
      systemModel.Remove();
      return xmlTemplate;
    }

    /// <summary>
    /// Iterates through all defined first level groups and adds them to the equipment model
    /// </summary>
    private void SetEquipmentGroups(ref XElement xmlModel, IModel model)
    {
      var equipmentStructure = model as EquipmentModeling.EquipmentModel;
      //get Group from template
      var equipmentGroup = GetEquipmentGroup();

      //add all equipment groups
      foreach (var group in equipmentStructure.EquipmentGroups)
      {
        equipmentGroup.Element(Constants.Name).Value = group.Name;
        equipmentGroup.Element(Constants.Guid).Value = group.InternalGuid;
        if (xmlModel.Element(Constants.Groups) == null)
        {
          xmlModel.Add(new XElement(Constants.Groups, equipmentGroup));
        }
        else
        {
          xmlModel.Element(Constants.Groups).Add(equipmentGroup);
        }
      }
    }

    /// <summary>
    /// Iterates through all the first level groups and sets the child groups recurively
    /// </summary>
    private void SetEquipmentSubGroups(ref XElement xmlModel, string groupId, List<EquipmentGroup> subGroups)
    {
      var xmlGroups = xmlModel.Descendants(Constants.Group);
      var xmlGroup = xmlGroups.Elements(Constants.Guid).FirstOrDefault(v => v.Value.Equals(groupId)).Parent;
      foreach (var subGroup in subGroups)
      {
        var equipmentGroup = GetEquipmentGroup();
        equipmentGroup.Element(Constants.Name).Value = subGroup.Name;
        equipmentGroup.Element(Constants.Guid).Value = subGroup.InternalGuid;
        if (xmlGroup.Element(Constants.ChildGroups) == null)
        {
          xmlGroup.Add(new XElement(Constants.ChildGroups, equipmentGroup));
        }
        else
        {
          xmlGroup.Element(Constants.ChildGroups).Add(equipmentGroup);
        }
        if (subGroup.EquipmentGroups != null && subGroup.EquipmentGroups.Count > 0)
        {
          SetEquipmentSubGroups(ref xmlModel, subGroup.InternalGuid, subGroup.EquipmentGroups);
        }
      }

    }
  }
}
