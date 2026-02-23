using Neomaster.PixelEarth.App;
using OpenTK.Graphics.OpenGL4;

namespace Neomaster.PixelEarth.Infra;

public static class BlendingExtensions
{
  private static Blending _blending = Blending.Undefined;

  public static void Apply(this Blending blending)
  {
    if (_blending == blending)
    {
      return;
    }

    _blending = blending;

    switch (blending)
    {
      case Blending.Alpha:
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        break;
      case Blending.Replace:
        GL.BlendFunc(BlendingFactor.One, BlendingFactor.Zero);
        break;
      default: throw blending.ArgumentOutOfRangeException();
    }
  }
}
