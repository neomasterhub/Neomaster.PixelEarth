using Neomaster.PixelEarth.Domain;

namespace Neomaster.PixelEarth.Presentation;

public class ShapeOptions : ICloneable<ShapeOptions>
{
  public CullFaces CullFaces { get; set; }
  public int ShaderProgramId { get; set; }
  public ShaderProgramArgs ShaderProgramArgs { get; set; }

  public ShapeOptions Clone()
  {
    return new ShapeOptions
    {
      CullFaces = CullFaces,
      ShaderProgramId = ShaderProgramId,
      ShaderProgramArgs = new ShaderProgramArgs
      {
        FillNormal = ShaderProgramArgs.FillNormal,
        FillHovered = ShaderProgramArgs.FillHovered,
        FillSelected = ShaderProgramArgs.FillSelected,
      },
    };
  }
}
