using Scada.AddIn.Contracts;
using System;
using System.Diagnostics;
using zenonExtensions;

namespace zenonExtensionsTest
{
  [AddInExtension("DynProperty Extensions", "Demo on how to use the Dynamic Property Extensions", "Samples")]
  public class EngineeringStudioWizardExtension : IEditorWizardExtension
  {
    #region IEditorWizardExtension implementation

    public void Run(IEditorApplication context, IBehavior behavior)
    {
      var project = context.Workspace.ActiveProject;

      var firstFontList = project.FontListCollection[0];
      var fontListName = firstFontList.GetName();
      Debug.Print($"The name of the first font list is: {fontListName}");

      var firstFont = firstFontList[0];
      var fontName = firstFont.GetName();
      var fontFont = firstFont.GetFont();
      var fontNumber = firstFont.GetNumber();
      Debug.Print($"The first font is called '{fontName}' and uses the system font '{fontFont}' and has the number {fontNumber}.");

    }

    #endregion
  }

}