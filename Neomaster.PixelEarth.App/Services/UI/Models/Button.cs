namespace Neomaster.PixelEarth.App;

public abstract class Button(int id)
  : UIElement
{
  public override int Id => id;
  public float Width { get; set; }
  public float Height { get; set; }
  public Action Action { get; set; }
}
