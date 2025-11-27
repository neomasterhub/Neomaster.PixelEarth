using Neomaster.PixelEarth.Presentation;
using OpenTK.Graphics.OpenGL4;
using N = System.Numerics;

namespace Neomaster.PixelEarth.Infra;

public static class ShaderProgramArgExtensions
{
  public static void BindColor4(
    this ShaderProgramArg<N.Vector4> arg,
    int shaderProgramId)
  {
    GL.Uniform4(
      GL.GetUniformLocation(shaderProgramId, arg.Name),
      arg.Value.ToColor4());
  }
}
