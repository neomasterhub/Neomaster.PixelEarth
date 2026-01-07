using Neomaster.PixelEarth.Presentation;
using SkiaSharp;

namespace Neomaster.PixelEarth.Infra;

public class ImageService : IImageService
{
  public ImageInfo GetImageInfo(string fileName)
  {
    var path = Path.Combine(InfraConsts.Dirs.Textures, fileName);
    using var bitmap = SKBitmap.Decode(path);

    return new ImageInfo
    {
      FilePath = path,
      Width = bitmap.Width,
      Height = bitmap.Height,
      Size = bitmap.ByteCount,
      Bytes = bitmap.Bytes,
    };
  }
}
