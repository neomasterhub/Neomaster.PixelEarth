using Neomaster.PixelEarth.Presentation;
using OpenTK.Graphics.OpenGL4;
using P = Neomaster.PixelEarth.Presentation;

namespace Neomaster.PixelEarth.Infra;

public class ShaderService : IShaderService
{
  public ShaderProgramInfo CreateShaderProgram(string vertexName, string fragmentName)
  {
    var vertex = CreateShader(vertexName, P.ShaderType.Vertex);
    var fragment = CreateShader(fragmentName, P.ShaderType.Fragment);
    var programId = GL.CreateProgram();

    GL.AttachShader(programId, vertex.Id);
    GL.AttachShader(programId, fragment.Id);
    GL.LinkProgram(programId);

    GL.GetProgram(programId, GetProgramParameterName.LinkStatus, out var status);
    if (status == 0)
    {
      var log = GL.GetProgramInfoLog(programId);
      throw new ShaderException(
        "Failed to create shader program.",
        log,
        new Dictionary<string, string>()
        {
          ["Vertex"] = vertexName,
          ["Fragment"] = fragmentName,
        });
    }

    GL.DeleteShader(vertex.Id);
    GL.DeleteShader(fragment.Id);

    return new ShaderProgramInfo
    {
      Id = programId,
    };
  }

  public ShaderInfo CreateShader(string name, P.ShaderType shaderType)
  {
    var script = GetScript(name);
    var id = GL.CreateShader(shaderType.ToGlType());

    GL.ShaderSource(id, script);
    GL.CompileShader(id);
    GL.GetShader(id, ShaderParameter.CompileStatus, out var status);

    if (status == 0)
    {
      var log = GL.GetShaderInfoLog(id);
      throw new ShaderException(
        "Failed to create shader.",
        log,
        new Dictionary<string, string>()
        {
          ["Name"] = name,
          ["Type"] = shaderType.ToString(),
        });
    }

    return new ShaderInfo
    {
      Id = id,
      Name = name,
      Type = shaderType,
    };
  }

  public string GetScript(string name)
  {
    var path = Path.Combine(InfraConsts.Dirs.Shaders, $"{name}.glsl");

    return File.ReadAllText(path);
  }
}
