using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.App;

public class RectangleTextureButton(int id)
  : TextureButton(id)
{
  private TextureShapeOptions _opt;
  private TextureShapeOptions _trOptBL;
  private TextureShapeOptions _trOptTR;

  public Rectangle Rectangle => new(X, Y, Width, Height);
  public TextureShapeOptions TextureShapeOptions_BL => _trOptBL;
  public TextureShapeOptions TextureShapeOptions_TR => _trOptTR;
  public override TextureShapeOptions TextureShapeOptions
  {
    get => _opt;
    set
    {
      _opt = value;
      _trOptBL = value.FromRectangleForTriangle_BL();
      _trOptTR = value.FromRectangleForTriangle_TR();
    }
  }
}
