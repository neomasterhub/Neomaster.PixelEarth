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
      Fill = new($"u{nameof(ShaderProgramArgs.Fill)}", Colors.Red),
    };
  }

  public static class Shape
  {
    public static readonly ShapeOptions DefaultOptions = new()
    {
      ShaderProgramArgs = Shader.DefaultProgramArgs,
    };
  }
}
