namespace Neomaster.PixelEarth.Presentation;

public interface IShaderService
{
  ShaderProgramInfo CreateShaderProgram(string vertexName, string fragmentName);
  ShaderInfo CreateShader(string name, ShaderType shaderType);
  string GetScript(string name);
}
