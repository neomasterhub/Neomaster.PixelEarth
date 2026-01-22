namespace Neomaster.PixelEarth.Presentation;

public interface ITextureService
{
  Blending Blending { get; }
  void Cut(int textureId); // TODO: draft
  TextureInfo Load(string fileName);
  void Load(Texture texture);
  void SetBlending(Blending blending);
  void Initialize();
}
