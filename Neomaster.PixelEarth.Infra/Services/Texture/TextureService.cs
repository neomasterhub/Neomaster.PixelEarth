using Neomaster.PixelEarth.App;
using OpenTK.Graphics.OpenGL4;

namespace Neomaster.PixelEarth.Infra;

public class TextureService(
  IImageService imageService,
  RenderSettings renderSettings)
  : ITextureService
{
  public Blending Blending { get; private set; }

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

  public void Load(TextureGroup textureGroup)
  {
    foreach (var texture in textureGroup.Textures)
    {
      Load(texture);
    }
  }

  public void Load(Texture texture)
  {
    var imageInfo = imageService.GetImageInfo(texture.FileName, flipY: true);

    texture.LoadedId = GL.GenTexture();
    texture.Width = imageInfo.Width;
    texture.Height = imageInfo.Height;

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

  public void SetBlending(Blending blending)
  {
    if (Blending == blending)
    {
      return;
    }

    Blending = blending;

    switch (blending)
    {
      case Blending.Alpha:
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        break;
      case Blending.Replace:
        GL.BlendFunc(BlendingFactor.One, BlendingFactor.Zero);
        break;
      default: throw blending.ArgumentOutOfRangeException();
    }
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
