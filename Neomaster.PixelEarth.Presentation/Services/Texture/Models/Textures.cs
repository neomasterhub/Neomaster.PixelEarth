namespace Neomaster.PixelEarth.Presentation;

public record Textures
{
  public TextureGroup[] TextureGroups;

  public Textures(params TextureGroup[] textureGroups)
  {
    TextureGroups = textureGroups;
  }
}
