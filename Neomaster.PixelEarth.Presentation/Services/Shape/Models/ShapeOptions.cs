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
}
