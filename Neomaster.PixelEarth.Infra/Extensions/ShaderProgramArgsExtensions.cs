using Neomaster.PixelEarth.Presentation;

namespace Neomaster.PixelEarth.Infra;

public static class ShaderProgramArgsExtensions
{
  public static void Bind(this ShaderProgramArgs args, int shaderProgramId)
  {
    args.FillNormal?.BindColor4(shaderProgramId);
    args.FillHovered?.BindColor4(shaderProgramId);
    args.FillSelected?.BindColor4(shaderProgramId);
    args.IsHovered?.BindBool(shaderProgramId);
    args.IsSelected?.BindBool(shaderProgramId);
  }
}
