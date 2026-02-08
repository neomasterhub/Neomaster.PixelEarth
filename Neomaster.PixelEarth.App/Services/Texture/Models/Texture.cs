namespace Neomaster.PixelEarth.App;

public record Texture
{
  private static readonly HashSet<int> _usedIds = [];

  public Texture(int id, string fileName)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(fileName);

    if (_usedIds.Contains(id))
    {
      throw new ArgumentException($"Texture with id \"{id}\" is already set.");
    }

    Id = id;
    FileName = fileName;
  }

  public int Id { get; }
  public string FileName { get; }
  public bool IsLoaded { get; set; }
  public int LoadedId { get; set; }
  public float Width { get; set; }
  public float Height { get; set; }
}
