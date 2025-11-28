using Neomaster.PixelEarth.Presentation;
using GlShaderType = OpenTK.Graphics.OpenGL4.ShaderType;

namespace Neomaster.PixelEarth.Infra;

public static class ShaderTypeExtensions
{
  public static ShaderType ToAppType(this GlShaderType type)
  {
    return type switch
    {
      GlShaderType.VertexShader => ShaderType.Vertex,
      GlShaderType.FragmentShader => ShaderType.Fragment,
      _ => throw new IndexOutOfRangeException(),
    };
  }

  public static GlShaderType ToGlType(this ShaderType type)
  {
    return type switch
    {
      ShaderType.Vertex => GlShaderType.VertexShader,
      ShaderType.Fragment => GlShaderType.FragmentShader,
      _ => throw new IndexOutOfRangeException(),
    };
  }
}
