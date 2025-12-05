using System.Numerics;

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

  public static readonly MenuItem[] MenuItems =
  [
    new() { Text = "Play" },
    new() { Text = "Exit" },
  ];

  public static class Colors
  {
    public static Vector4 Red = new(1, 0, 0, 1);
    public static Vector4 Green = new(0, 1, 0, 1);
    public static Vector4 Blue = new(0, 0, 1, 1);
    public static Vector4 Background = new(0.2f, 0.4f, 0.6f, 1.0f);
  }

  public static class Shader
  {
    public static readonly ShaderProgramArgs DefaultProgramArgs = new()
    {
      FillNormal = new($"u{nameof(ShaderProgramArgs.FillNormal)}", Colors.Red),
      FillHovered = new($"u{nameof(ShaderProgramArgs.FillHovered)}", new(1, 0.4f, 0, 1)),
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

  public static class Buttons
  {
    public static readonly ButtonOptions DefaultOptions = new()
    {
      FillNormal = new(0.929f, 0.929f, 0.929f, 1),
      FillHovered = new(0.890f, 0.890f, 0.886f, 1),
      FillSelected = new(0.839f, 0.859f, 0.906f, 1f),
      FillSelectedHovered = new(0.796f, 0.824f, 0.882f, 1),
    };
  }
}
