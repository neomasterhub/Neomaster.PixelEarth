using Neomaster.PixelEarth.Presentation;
using OpenTK.Graphics.OpenGL4;

namespace Neomaster.PixelEarth.Infra;

public class TextureService(
  IImageService imageService,
  RenderSettings renderSettings)
  : ITextureService
{
  public void Cut(int textureId)
  {
    var width = 200;

    GL.BindTexture(TextureTarget.Texture2D, textureId);
    GL.TexSubImage2D(
      TextureTarget.Texture2D,
      0,
      0,
      0,
      width,
      width,
      PixelFormat.Rgba,
      PixelType.UnsignedByte,
      new byte[width * width * 4]);
    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
  }

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

    Cut(textureId); // TODO: remove

    return new TextureInfo
    {
      Id = textureId,
    };
  }

  public void Load(Texture texture)
  {
    var imageInfo = imageService.GetImageInfo(texture.FileName, flipY: true);

    texture.LoadedId = GL.GenTexture();

    GL.BindTexture(TextureTarget.Texture2D, texture.LoadedId);
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

    texture.IsLoaded = true;
  }

  public void Initialize()
  {
    if (renderSettings.AlphaBlendingEnabled)
    {
      GL.Enable(EnableCap.Blend);
    }
    else
    {
      GL.Disable(EnableCap.Blend);
    }
  }
}
