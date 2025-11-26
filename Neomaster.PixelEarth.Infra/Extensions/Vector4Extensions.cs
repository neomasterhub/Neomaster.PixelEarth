using OpenTK.Mathematics;
using SystemV4 = System.Numerics.Vector4;

namespace Neomaster.PixelEarth.Infra;

public static class Vector4Extensions
{
  public static Color4 ToColor4(this SystemV4 v)
  {
    return new Color4(v.X, v.Y, v.Z, v.W);
  }
}
