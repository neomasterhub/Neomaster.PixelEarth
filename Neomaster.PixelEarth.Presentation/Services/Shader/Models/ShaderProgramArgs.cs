using System.Numerics;

namespace Neomaster.PixelEarth.Presentation;

public struct ShaderProgramArgs
{
  public ShaderProgramArg<Vector4> FillNormal;
  public ShaderProgramArg<Vector4> FillHovered;
  public ShaderProgramArg<bool> IsHovered;
}
