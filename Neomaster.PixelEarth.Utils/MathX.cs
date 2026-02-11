using System;
using System.Numerics;

namespace Neomaster.PixelEarth.Utils;

public static class MathX
{
  public static float FittableCount(
    float maxLength,
    float elementLength,
    float gap)
  {
    // L = N * (l + g) - g
    // N = floor((L + g) / (l + g))
    return MathF.Floor((maxLength + gap) / (elementLength + gap));
  }

  public static float FittableLength(
    float elementCount,
    float elementLength,
    float gap)
  {
    return (elementCount * (elementLength + gap)) - gap;
  }

  public static class Rectangle
  {
    public static class Points
    {
      public static Vector2[] TopRight_BottomLeft(Vector4 xywh)
      {
        return [new(xywh.X, xywh.Y), new(xywh.X + xywh.Z, xywh.Y + xywh.W)];
      }

      public static Vector2[] TopRight_BottomLeft(float x, float y, float width, float height)
      {
        return [new(x, y), new(x + width, y + height)];
      }
    }

    public static Vector2[] GetTrianglePoints_TopRight(Vector2[] topLeft_bottomRight)
    {
      return GetTrianglePoints_TopRight(topLeft_bottomRight[0], topLeft_bottomRight[1]);
    }

    public static Vector2[] GetTrianglePoints_BottomLeft(Vector2[] topLeft_bottomRight)
    {
      return GetTrianglePoints_BottomLeft(topLeft_bottomRight[0], topLeft_bottomRight[1]);
    }

    public static Vector2[] GetTrianglePoints_TopRight(Vector2 topLeft, Vector2 bottomRight)
    {
      return [topLeft, bottomRight, new(bottomRight.X, topLeft.Y)];
    }

    public static Vector2[] GetTrianglePoints_BottomLeft(Vector2 topLeft, Vector2 bottomRight)
    {
      return [topLeft, new(topLeft.X, bottomRight.Y), bottomRight];
    }
  }
}
