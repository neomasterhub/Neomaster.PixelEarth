namespace Neomaster.PixelEarth.Presentation;

public class TextureGroup
{
  private static readonly HashSet<string> _usedNames = [];

  private readonly HashSet<Texture> _textures = [];

  public TextureGroup(string name)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(name);

    if (_usedNames.Contains(name))
    {
      throw new ArgumentException($"Texture group with name \"{name}\" is already set.");
    }

    Name = name;
  }

  public string Name { get; }

  public IReadOnlyList<Texture> Textures => _textures.ToList();

  public TextureGroup AddTexture(Texture texture)
  {
    _textures.Add(texture);
    return this;
  }
}
