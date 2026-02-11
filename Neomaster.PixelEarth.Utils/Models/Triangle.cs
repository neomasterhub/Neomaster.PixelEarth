using System.Numerics;

namespace Neomaster.PixelEarth.Utils;

public struct Triangle
{
  public Vector2 A;
  public Vector2 B;
  public Vector2 C;

  public Triangle(Vector2 a, Vector2 b, Vector2 c)
  {
    A = a;
    B = b;
    C = c;
  }

  public readonly Vector2[] GetVerticies()
  {
    return [A, B, C];
  }
}
