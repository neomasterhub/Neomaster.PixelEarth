namespace Neomaster.PixelEarth.Presentation;

public class TextureGroup
{
  private readonly HashSet<Texture> _textures = [];

  public TextureGroup(string name)
  {
    Name = name;
  }

  public string Name { get; }

  public TextureGroup AddTexture(Texture texture)
  {
    _textures.Add(texture);
    return this;
  }
}
