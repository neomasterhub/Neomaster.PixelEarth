namespace Neomaster.PixelEarth.App;

public class TextureButton(int id)
  : UIElement
{
  public override int Id => id;
  public float Width { get; set; }
  public float Height { get; set; }
  public Action Action { get; set; }
  public TextureButtonOptions Options { get; set; }
}
