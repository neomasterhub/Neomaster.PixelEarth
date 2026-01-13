namespace Neomaster.PixelEarth.Presentation;

public record TextureGroup
{
  private static readonly List<string> _usedNames = [];
  public string Name { get; }
  public Texture[] Textures { get; }

  public TextureGroup(string name, params Texture[] textures)
  {
    if (_usedNames.Contains(name))
    {
      throw new ArgumentException($"Texture group with name \"{name}\" already exists.");
    }

    _usedNames.Add(name);

    Name = name;
    Textures = textures;
  }
}
