using Neomaster.PixelEarth.App;
using OpenTK.Graphics.OpenGL4;

namespace Neomaster.PixelEarth.Infra;

public static class RenderSettingsExtensions
{
  public static void Apply(this RenderSettings rs)
  {
    GL.FrontFace(rs.WindingOrder.ToGlType());

    if (rs.AlphaBlendingEnabled)
    {
      GL.Enable(EnableCap.Blend);
    }
    else
    {
      GL.Disable(EnableCap.Blend);
    }
  }
}
