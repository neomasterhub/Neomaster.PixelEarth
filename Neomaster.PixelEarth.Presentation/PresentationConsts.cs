using System.Numerics;
using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.Presentation;

public static class PresentationConsts
{
  public static readonly RenderSettings RenderSettings = new()
  {
    WindingOrder = WindingOrder.CounterClockwise,
  };

  public static readonly WindowSettings WindowSettings = new()
  {
    Width = 800,
    Height = 600,
    Title = "Pixel Earth",
  };

  public static class Color
  {
    public static Vector4 Red = new(1, 0, 0, 1);
    public static Vector4 Green = new(0, 1, 0, 1);
    public static Vector4 Blue = new(0, 0, 1, 1);
    public static Vector4 Background = new(0.2f, 0.4f, 0.6f, 1.0f);
    public static Vector4 FillNormal = ColorHelper.HexToVector4("81d4fa00");
    public static Vector4 FillHovered = ColorHelper.HexToVector4("64b5f600");
    public static Vector4 FillSelected = ColorHelper.HexToVector4("eceff100");
    public static Vector4 FillSelectedHovered = ColorHelper.HexToVector4("cfd8dc00");
  }

  public static class Shader
  {
    public static readonly ShaderProgramArgs DefaultProgramArgs = new()
    {
      FillNormal = new($"u{nameof(ShaderProgramArgs.FillNormal)}", Color.FillNormal),
      FillHovered = new($"u{nameof(ShaderProgramArgs.FillHovered)}", Color.FillHovered),
      IsHovered = new($"u{nameof(ShaderProgramArgs.IsHovered)}", false),
    };
  }

  public static class Shape
  {
    public static ShapeOptions DefaultOptions = new()
    {
      ShaderProgramArgs = Shader.DefaultProgramArgs,
    };
  }

  public static class Button
  {
    public static readonly ButtonOptions DefaultOptions = new()
    {
      FillNormal = Color.FillNormal,
      FillHovered = Color.FillHovered,
      FillSelected = Color.FillSelected,
      FillSelectedHovered = Color.FillSelectedHovered,
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
