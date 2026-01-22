using Neomaster.PixelEarth.App;
using OpenTK.Graphics.OpenGL4;

namespace Neomaster.PixelEarth.Infra;

public class ShaderService : IShaderService
{
  private static readonly object _shaderInitLock = new();
  private static volatile bool _shadersInitialized = false;

  public ColorShaderProgramInfo ColorShaderProgramInfo { get; private set; }
  public TextureShaderProgramInfo TextureShaderProgramInfo { get; private set; }

  public ShaderProgramInfo CreateShaderProgram(string vertexName, string fragmentName)
  {
    var vertex = CreateShader(vertexName, App.ShaderType.Vertex);
    var fragment = CreateShader(fragmentName, App.ShaderType.Fragment);
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

  public ShaderInfo CreateShader(string name, App.ShaderType shaderType)
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

  public void InitializeShaders()
  {
    lock (_shaderInitLock)
    {
      if (_shadersInitialized)
      {
        throw new InvalidOperationException("Shaders have already been initialized.");
      }

      var colorTriangleProgram = CreateShaderProgram("color.vertex", "color.fragment");
      ColorShaderProgramInfo = new ColorShaderProgramInfo
      {
        Id = colorTriangleProgram.Id,
        ColorUniformName = "uColor",
      };

      var textureTriangleProgram = CreateShaderProgram("texture.vertex", "texture.fragment");
      TextureShaderProgramInfo = new TextureShaderProgramInfo
      {
        Id = textureTriangleProgram.Id,
        TextureUniformName = "uTexture",
      };

      _shadersInitialized = true;
    }
  }

  public bool ShadersInitialized()
  {
    return _shadersInitialized;
  }
}
