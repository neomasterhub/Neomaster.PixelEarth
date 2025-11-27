using Neomaster.PixelEarth.Presentation;
using OpenTK.Graphics.OpenGL4;

namespace Neomaster.PixelEarth.Infra;

public class ShaderService : IShaderService
{
  public ShaderInfo Create(string name, Presentation.ShaderType shaderType)
  {
    var script = GetScript(name);
    var id = GL.CreateShader(shaderType.ToGlType());

    GL.ShaderSource(id, script);
    GL.CompileShader(id);
    GL.GetShader(id, ShaderParameter.CompileStatus, out var status);

    if (status == 0)
    {
      var log = GL.GetShaderInfoLog(id);
      throw new FileLoadException(log);
    }

    return new ShaderInfo
    {
      Id = id,
      Name = name,
    };
  }

  public string GetScript(string name)
  {
    var path = Path.Combine(InfraConsts.Dirs.Shaders, $"{name}.glsl");

    return File.ReadAllText(path);
  }
}
