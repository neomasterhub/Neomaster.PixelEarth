using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Presentation;

public class TexturesBuilder
{
  private readonly Textures _textures = new();

  private TexturesBuilder()
  {
  }

  public static TexturesBuilder Create()
  {
    return new();
  }
}
