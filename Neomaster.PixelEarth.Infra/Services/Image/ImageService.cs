using Neomaster.PixelEarth.App;
using SkiaSharp;

namespace Neomaster.PixelEarth.Infra;

public class ImageService : IImageService
{
  public ImageInfo GetImageInfo(
    string fileName,
    bool flipX = false,
    bool flipY = false)
  {
    var path = Path.Combine(InfraConsts.Dirs.Textures, fileName);
    using var bitmapSrc = SKBitmap.Decode(path);
    var bitmapResult = bitmapSrc;

    if (flipX || flipY)
    {
      bitmapResult = new SKBitmap(bitmapSrc.Width, bitmapSrc.Height, SKColorType.Rgba8888, SKAlphaType.Premul);
      using var canvas = new SKCanvas(bitmapResult);

      var scaleX = flipX ? -1f : 1f;
      var scaleY = flipY ? -1f : 1f;
      canvas.Scale(scaleX, scaleY);

      var translateX = flipX ? -bitmapSrc.Width : 0;
      var translateY = flipY ? -bitmapSrc.Height : 0;
      canvas.Translate(translateX, translateY);

      canvas.DrawBitmap(bitmapSrc, 0, 0);
    }

    var result = new ImageInfo
    {
      FilePath = path,
      Width = bitmapResult.Width,
      Height = bitmapResult.Height,
      Size = bitmapResult.ByteCount,
      Bytes = bitmapResult.Bytes,
    };

    return result;
  }
}
