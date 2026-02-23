namespace Neomaster.PixelEarth.App;

public interface ITextureService
{
  void Cut(int textureId); // TODO: draft
  void Load(TextureGroup textureGroup);
  void Load(Texture texture);
}
