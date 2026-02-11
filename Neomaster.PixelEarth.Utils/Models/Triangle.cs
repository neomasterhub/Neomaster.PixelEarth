using System.Numerics;

namespace Neomaster.PixelEarth.Utils;

public record Triangle
{
  public Triangle()
  {
  }

  public Triangle(Vector2 a, Vector2 b, Vector2 c)
  {
    A = a;
    B = b;
    C = c;
  }

  public Vector2 A { get; set; }
  public Vector2 B { get; set; }
  public Vector2 C { get; set; }

  public Vector2[] GetVerticies()
  {
    return [A, B, C];
  }
}
