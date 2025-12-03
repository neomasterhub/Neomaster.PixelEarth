namespace Neomaster.PixelEarth.Presentation;

public record ShapeOptions
{
  public int ShaderProgramId { get; set; }
  public ShaderProgramArgs ShaderProgramArgs { get; set; }
  public CullFaces CullFaces { get; set; }
}
