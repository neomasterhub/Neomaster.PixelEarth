using Neomaster.PixelEarth.Presentation;
using SkiaSharp;

namespace Neomaster.PixelEarth.Infra;

public class TextureService : ITextureService
{
  public TextureInfo Load(string fileName)
  {
    var path = Path.Combine(InfraConsts.Dirs.Textures, fileName);
    using var original = SKBitmap.Decode(path);

    return new TextureInfo
    {
      Path = path,
    };
  }
}
