using System.Numerics;

namespace Neomaster.PixelEarth.Presentation;

public record ShaderProgramArgs
{
  public ShaderProgramArg<Vector4> FillNormal { get; set; }
  public ShaderProgramArg<Vector4> FillHovered { get; set; }
  public ShaderProgramArg<Vector4> FillSelected { get; set; }
}
