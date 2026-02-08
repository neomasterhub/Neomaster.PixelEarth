namespace Neomaster.PixelEarth.App;

public class Textures
{
  private readonly HashSet<TextureGroup> _textureGroups = [];

  public IReadOnlyList<TextureGroup> TextureGroups => _textureGroups.ToList();

  public TextureGroup this[int textureGroupId]
  {
    get => _textureGroups.Single(g => g.Id == textureGroupId);
  }

  public Textures AddGroup(TextureGroup group)
  {
    _textureGroups.Add(group);
    return this;
  }
}
