using Scada.AddIn.Contracts;

namespace XmlImporter
{
  interface ICreator
  {
    void Create(IEditorApplication context, IModel model);
  }
}
