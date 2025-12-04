using Neomaster.PixelEarth.Presentation;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using S = System.Numerics;

namespace Neomaster.PixelEarth.Infra;

public static class ShaderProgramArgExtensions
{
  public static void BindBool(
    this ShaderProgramArg<bool> arg,
    int shaderProgramId)
  {
    GL.Uniform1(
      GL.GetUniformLocation(shaderProgramId, arg.Name),
      arg.Value ? 1 : 0);
  }

  public static void BindColor4(
    this ShaderProgramArg<S.Vector4> arg,
    int shaderProgramId)
  {
    GL.Uniform4(
      GL.GetUniformLocation(shaderProgramId, arg.Name),
      arg.Value.ToColor4());
  }

  public static void BindMatrix4(
    this ShaderProgramArg<Matrix4> arg,
    int shaderProgramId,
    bool transpose = false)
  {
    var matrix = arg.Value;
    GL.UniformMatrix4(
      GL.GetUniformLocation(shaderProgramId, arg.Name),
      transpose,
      ref matrix);
  }
}
