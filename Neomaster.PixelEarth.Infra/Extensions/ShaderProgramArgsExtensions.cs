using Neomaster.PixelEarth.Presentation;

namespace Neomaster.PixelEarth.Infra;

public static class ShaderProgramArgsExtensions
{
  public static void Bind(this ShaderProgramArgs args, int shaderProgramId)
  {
    args.Fill?.BindColor4(shaderProgramId);
  }
}
