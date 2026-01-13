namespace Neomaster.PixelEarth.Presentation;

public class Textures
{
  private readonly HashSet<TextureGroup> _textureGroups = [];

  public Textures AddGroup(TextureGroup group)
  {
    _textureGroups.Add(group);
    return this;
  }
}
