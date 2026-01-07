using Neomaster.PixelEarth.Presentation;
using OpenTK.Graphics.OpenGL4;

namespace Neomaster.PixelEarth.Infra;

public static class ShapeOptionsExtensions
{
  public static void UseWithProgram(this ColorShapeOptions options)
  {
    GL.UseProgram(options.ShaderProgramId);
    options.ShaderColor.BindColor4(options.ShaderProgramId);
  }

  public static void UseWithProgram(this TextureShapeOptions options)
  {
    GL.UseProgram(options.ShaderProgramId);
  }
}
