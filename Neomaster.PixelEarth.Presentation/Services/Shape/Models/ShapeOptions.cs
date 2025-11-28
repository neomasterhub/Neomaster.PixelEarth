namespace Neomaster.PixelEarth.Presentation;

public record ShapeOptions
{
  public int ShaderProgramId { get; set; }
  public ShaderProgramArgs ShaderProgramArgs { get; set; }
  public DrawMode DrawMode { get; set; }
  public DrawMode FrontSideDrawMode { get; set; }
  public bool ShowFrontSide { get; set; }
  public bool ShowBackSide { get; set; }
}
