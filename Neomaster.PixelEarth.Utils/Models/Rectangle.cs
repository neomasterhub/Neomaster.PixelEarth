using System.Numerics;

namespace Neomaster.PixelEarth.Utils;

public struct Rectangle
{
  public float X;
  public float Y;
  public float Width;
  public float Height;

  public Rectangle(float x, float y, float width, float height)
  {
    X = x;
    Y = y;
    Width = width;
    Height = height;
  }

  public Rectangle(Vector4 v)
  {
    X = v.X;
    Y = v.Y;
    Width = v.Z;
    Height = v.W;
  }

  public readonly (Triangle tr, Triangle bl) GetTriangles()
  {
    return (
      new(new(X, Y), new(X + Width, Y + Height), new(X + Width, Y)),
      new(new(X, Y), new(X, Y + Height), new(X + Width, Y + Height)));
  }

  public readonly Vector2[] GetVerticies_TL_BR()
  {
    return [new(X, Y), new(X + Width, Y + Height)];
  }
}
