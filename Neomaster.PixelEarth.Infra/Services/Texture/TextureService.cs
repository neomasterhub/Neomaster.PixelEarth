using Neomaster.PixelEarth.Presentation;
using OpenTK.Graphics.OpenGL4;

namespace Neomaster.PixelEarth.Infra;

public class TextureService(
  IImageService imageService)
  : ITextureService
{
  public TextureInfo Load(string fileName)
  {
    var imageInfo = imageService.GetImageInfo(fileName, flipY: true);

    var textureId = GL.GenTexture();

    GL.BindTexture(TextureTarget.Texture2D, textureId);
    GL.TexImage2D(
      target: TextureTarget.Texture2D,
      level: 0,
      PixelInternalFormat.Rgba,
      imageInfo.Width,
      imageInfo.Height,
      border: 0,
      PixelFormat.Rgba,
      PixelType.UnsignedByte,
      imageInfo.Bytes);
    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

    return new TextureInfo
    {
      Id = textureId,
    };
  }
}
