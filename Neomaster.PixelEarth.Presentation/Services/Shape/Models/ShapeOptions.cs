using System.Numerics;

namespace Neomaster.PixelEarth.Presentation;

public struct ShapeOptions
{
  public int ShaderProgramId; // TODO: rename to "FillShaderProgramId"
  public int TextureShaderProgramId;

  public CullFaces CullFaces;
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
