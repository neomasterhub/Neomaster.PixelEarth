using OpenTK.Mathematics;
using S = System.Numerics;

namespace Neomaster.PixelEarth.Infra;

public static class VectorExtensions
{
  public static Color4 ToColor4(this S.Vector4 v)
  {
    return new Color4(v.X, v.Y, v.Z, v.W);
  }

  public static Vector2 ToGlUv(this S.Vector2 v)
  {
    return new Vector2(v.X, 1f - v.Y);
  }
}
