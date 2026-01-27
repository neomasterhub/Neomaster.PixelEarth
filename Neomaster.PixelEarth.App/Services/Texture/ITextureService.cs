namespace Neomaster.PixelEarth.App;

public interface ITextureService
{
  Blending Blending { get; }
  void Cut(int textureId); // TODO: draft
  void Load(TextureGroup textureGroup);
  void Load(Texture texture);
  void SetBlending(Blending blending);
  void Initialize();
}
