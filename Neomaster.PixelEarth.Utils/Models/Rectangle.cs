using System.Numerics;

namespace Neomaster.PixelEarth.Utils;

public record Rectangle
{
  public Rectangle()
  {
  }

  public Rectangle(Vector4 v)
  {
    X = v.X;
    Y = v.Y;
    Width = v.Z;
    Height = v.W;
  }

  public float X { get; set; }
  public float Y { get; set; }
  public float Width { get; set; }
  public float Height { get; set; }

  public Vector2[] GetVerticies_TL_BR()
  {
    return [new(X, Y), new(X + Width, Y + Height)];
  }
}
