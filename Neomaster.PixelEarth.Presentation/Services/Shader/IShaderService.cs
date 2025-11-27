namespace Neomaster.PixelEarth.Presentation;

public interface IShaderService
{
  ShaderInfo Create(string name, ShaderType shaderType);
  string GetScript(string name);
}
