using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Presentation;

public class TexturesBuilder
{
  private readonly Textures _textures = new();

  private TextureGroup _lastTextureGroup;

  private TexturesBuilder()
  {
  }

  public static TexturesBuilder Create()
  {
    return new();
  }

  public TexturesBuilder AddTextureGroup(TextureGroupId id)
  {
    _lastTextureGroup = new TextureGroup((int)id);
    _textures.AddGroup(_lastTextureGroup);

    return this;
  }
}
