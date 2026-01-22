namespace Neomaster.PixelEarth.Presentation;

public class Textures
{
  private readonly HashSet<TextureGroup> _textureGroups = [];

  public IReadOnlyList<TextureGroup> TextureGroups => _textureGroups.ToList();

  public TextureGroup this[string textureGroupName]
  {
    get => _textureGroups.Single(g => g.Name == textureGroupName);
  }

  public Textures AddGroup(TextureGroup group)
  {
    _textureGroups.Add(group);
    return this;
  }
}
