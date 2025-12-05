using System.Numerics;

namespace Neomaster.PixelEarth.Presentation;

public struct ShapeOptions
{
  public CullFaces CullFaces;
  public int ShaderProgramId;
  public ShaderProgramArgs ShaderProgramArgs;

  public ShapeOptions IsHovered(bool isHovered)
  {
    ShaderProgramArgs.IsHovered.Value = isHovered;

    return this;
  }

  public ShapeOptions FillNormal(Vector4 color)
  {
    ShaderProgramArgs.FillNormal.Value = color;

    return this;
  }

  public ShapeOptions FillHovered(Vector4 color)
  {
    ShaderProgramArgs.FillHovered.Value = color;

    return this;
  }
}
