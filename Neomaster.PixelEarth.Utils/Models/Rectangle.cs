using System;
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

  public Rectangle(Vector2[] tl_br)
  {
    if (tl_br.Length != 2)
    {
      throw new InvalidOperationException(
        "The array must contain exactly two points: [top-left, bottom-right].");
    }

    X = tl_br[0].X;
    Y = tl_br[0].Y;
    Width = tl_br[1].X - X;
    Height = tl_br[1].Y - Y;
  }

  public readonly Triangle GetTriangle_TR()
  {
    return new(new(X, Y), new(X + Width, Y + Height), new(X + Width, Y));
  }

  public readonly Triangle GetTriangle_BL()
  {
    return new(new(X, Y), new(X, Y + Height), new(X + Width, Y + Height));
  }

  public readonly Vector2[] GetVerticies_TL_BR()
  {
    return [new(X, Y), new(X + Width, Y + Height)];
  }
}
