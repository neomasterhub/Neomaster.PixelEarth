using System.Numerics;
using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.Presentation;

public static class PresentationConsts
{
  public static readonly RenderSettings RenderSettings = new()
  {
    WindingOrder = WindingOrder.CounterClockwise,
    AlphaBlendingEnabled = true,
  };

  public static readonly WindowSettings WindowSettings = new()
  {
    Width = 800,
    Height = 600,
    Title = "Pixel Earth",
  };

  public static class Color
  {
    public static readonly Vector4 Red = new(1, 0, 0, 1);
    public static readonly Vector4 Green = new(0, 1, 0, 1);
    public static readonly Vector4 Blue = new(0, 0, 1, 1);
    public static readonly Vector4 Background = new(0.2f, 0.4f, 0.6f, 1.0f);
    public static readonly Vector4 ControlFill = ColorHelper.HexToVector4("eceff100");
    public static readonly Vector4 ControlFillHovered = ColorHelper.HexToVector4("cfd8dc00");
    public static readonly Vector4 ControlFillSelected = ColorHelper.HexToVector4("81d4fa00");
    public static readonly Vector4 ControlFillSelectedHovered = ColorHelper.HexToVector4("64b5f600");
  }

  public static class Shape
  {
    public static readonly ColorShapeOptions ColorDefaultOptions = new()
    {
      Color = Color.ControlFill,
      ColorHovered = Color.ControlFillHovered,
      ColorSelected = Color.ControlFillSelected,
      ColorSelectedHovered = Color.ControlFillSelectedHovered,
    };

    public static readonly TextureShapeOptions TextureDefaultOptions = new()
    {
    };
  }

  public static class Button
  {
    public static readonly ButtonOptions DefaultOptions = new()
    {
      Fill = Color.ControlFill,
      FillHovered = Color.ControlFillHovered,
      FillSelected = Color.ControlFillSelected,
      FillSelectedHovered = Color.ControlFillSelectedHovered,
    };
  }

  public static class MainMenu
  {
    public static readonly MainMenuOptions DefaultOptions = new()
    {
      ButtonGap = 15,
      ButtonWidth = 200,
      ButtonHeight = 50,
      VerticalAlign = Align.Center,
      HorizontalAlign = Align.Center,
    };
  }
}
