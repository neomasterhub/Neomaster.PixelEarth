namespace Neomaster.PixelEarth.App;

public class TextureGroup
{
  private static readonly HashSet<int> _usedIds = [];

  private readonly HashSet<Texture> _textures = [];

  public TextureGroup(int id)
  {
    if (_usedIds.Contains(id))
    {
      throw new ArgumentException($"Texture group with name \"{id}\" is already set.");
    }

    Id = id;
  }

  public int Id { get; }

  public IReadOnlyList<Texture> Textures => _textures.ToList();

  public Texture this[int textureId] => _textures.Single(t => t.Id == textureId);

  public TextureGroup AddTexture(Texture texture)
  {
    _textures.Add(texture);
    return this;
  }
}
