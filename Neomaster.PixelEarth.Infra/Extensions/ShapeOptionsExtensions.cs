using Neomaster.PixelEarth.Presentation;
using OpenTK.Graphics.OpenGL4;

namespace Neomaster.PixelEarth.Infra;

public static class ShapeOptionsExtensions
{
  public static void UseWithProgram(this ShapeOptions options)
  {
    GL.UseProgram(options.ShaderProgramId);
    options.ShaderProgramArgs.Bind(options.ShaderProgramId);
  }
}
