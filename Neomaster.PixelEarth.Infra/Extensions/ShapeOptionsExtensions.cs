using Neomaster.PixelEarth.App;
using OpenTK.Graphics.OpenGL4;

namespace Neomaster.PixelEarth.Infra;

public static class ShapeOptionsExtensions
{
  public static void UseWithShaderProgram(
    this ColorShapeOptions options,
    ColorShaderProgramInfo colorShaderProgramInfo)
  {
    GL.UseProgram(colorShaderProgramInfo.Id);
    options
      .GetShaderColor(colorShaderProgramInfo.ColorUniformName)
      .BindColor4(colorShaderProgramInfo.Id);
  }

  public static void UseWithShaderProgram(
    this TextureShapeOptions options,
    TextureShaderProgramInfo textureShaderProgramInfo)
  {
    GL.UseProgram(textureShaderProgramInfo.Id);
  }
}
