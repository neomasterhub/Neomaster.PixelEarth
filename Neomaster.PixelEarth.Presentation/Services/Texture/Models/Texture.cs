namespace Neomaster.PixelEarth.Presentation;

public record Texture
{
  public Texture(string name, string fileName)
  {
    Name = name;
    FileName = fileName;
  }

  public string Name { get; }
  public string FileName { get; }
  public bool IsLoaded { get; set; }
  public int LoadedId { get; set; }
}
