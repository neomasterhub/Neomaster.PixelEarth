using Neomaster.PixelEarth.App;
using OpenTK.Graphics.OpenGL4;

namespace Neomaster.PixelEarth.Infra;

public static class WindingOrderExtensions
{
  public static FrontFaceDirection ToGlType(this WindingOrder windingOrder)
  {
    return windingOrder switch
    {
      WindingOrder.CounterClockwise => FrontFaceDirection.Ccw,
      WindingOrder.Clockwise => FrontFaceDirection.Cw,
      _ => throw windingOrder.ArgumentOutOfRangeException(),
    };
  }
}
