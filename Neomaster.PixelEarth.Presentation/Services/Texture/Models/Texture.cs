namespace Neomaster.PixelEarth.Presentation;

public record Texture
{
  public string FileName { get; init; }
  public bool IsLoaded { get; set; }
  public int LoadedId { get; set; }
}
