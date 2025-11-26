using System.Numerics;

namespace Neomaster.PixelEarth.Presentation;

public static class PresentationConsts
{
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
    public static Vector4 Background = new(0.2f, 0.4f, 0.6f, 1.0f);
  }
}
