using System.Numerics;

namespace Neomaster.PixelEarth.Presentation;

public record ShaderProgramArgs
{
  public ShaderProgramArg<Vector4> Fill { get; set; }
}
