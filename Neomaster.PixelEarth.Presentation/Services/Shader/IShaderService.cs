namespace Neomaster.PixelEarth.Presentation;

public interface IShaderService
{
  ColorShaderProgramInfo ColorShaderProgramInfo { get; }
  TextureShaderProgramInfo TextureShaderProgramInfo { get; }
  ShaderProgramInfo CreateShaderProgram(string vertexName, string fragmentName);
  ShaderInfo CreateShader(string name, ShaderType shaderType);
  string GetScript(string name);
  void InitializeShaders();
  bool ShadersInitialized();
}
