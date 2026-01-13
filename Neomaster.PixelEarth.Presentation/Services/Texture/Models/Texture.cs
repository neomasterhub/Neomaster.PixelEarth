namespace Neomaster.PixelEarth.Presentation;

public record Texture
{
  private static readonly HashSet<string> _usedNames = [];

  public Texture(string name, string fileName)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(name);
    ArgumentException.ThrowIfNullOrWhiteSpace(fileName);

    if (_usedNames.Contains(name))
    {
      throw new ArgumentException($"Texture with name \"{name}\" is already set.");
    }

    Name = name;
    FileName = fileName;
  }

  public string Name { get; }
  public string FileName { get; }
  public bool IsLoaded { get; set; }
  public int LoadedId { get; set; }
}
