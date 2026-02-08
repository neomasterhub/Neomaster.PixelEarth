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

  public TexturesBuilder WithTexture(TextureId id, string fileName)
  {
    if (_lastTextureGroup == null)
    {
      throw new InvalidOperationException(
        "Target texture group should be initialized before adding textures.");
    }

    _lastTextureGroup.AddTexture(new((int)id, fileName));

    return this;
  }

  public Textures Build()
  {
    return _textures;
  }
}
