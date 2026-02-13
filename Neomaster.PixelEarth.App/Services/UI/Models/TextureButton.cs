namespace Neomaster.PixelEarth.App;

public abstract class TextureButton(int id)
  : Button(id)
{
  public virtual TextureShapeOptions TextureShapeOptions { get; set; }
}
