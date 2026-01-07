namespace Neomaster.PixelEarth.Presentation;

public interface IImageService
{
  ImageInfo GetImageInfo(
    string fileName,
    bool flipX = false,
    bool flipY = false);
}
