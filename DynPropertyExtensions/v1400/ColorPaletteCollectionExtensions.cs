//AUTOGENERATED FILE. Do not make any manual changes. Any changes to this file will be overwritten.

using Scada.AddIn.Contracts.ColorPalette;

namespace zenonExtensions
{
  public static class ColorPaletteCollectionExtension
  {
/// Sets Color name
    public static void SetColorNames(this IColorPaletteCollection colorPalettes, string value)
    {
      colorPalettes.SetDynamicProperty("ColorNames", value);
    }

/// Gets Color name
    public static string GetColorNames(this IColorPaletteCollection colorPalettes)
    {
      return (string) colorPalettes.GetDynamicProperty("ColorNames");
    }

/// Sets Palette name
    public static void SetPaletteName(this IColorPaletteCollection colorPalettes, string value)
    {
      colorPalettes.SetDynamicProperty("PaletteName", value);
    }

/// Gets Palette name
    public static string GetPaletteName(this IColorPaletteCollection colorPalettes)
    {
      return (string) colorPalettes.GetDynamicProperty("PaletteName");
    }

/// Sets Colors of the palette
    public static void SetPaletteColors(this IColorPaletteCollection colorPalettes, string value)
    {
      colorPalettes.SetDynamicProperty("PaletteColors", value);
    }

/// Gets Colors of the palette
    public static string GetPaletteColors(this IColorPaletteCollection colorPalettes)
    {
      return (string) colorPalettes.GetDynamicProperty("PaletteColors");
    }

  }
}