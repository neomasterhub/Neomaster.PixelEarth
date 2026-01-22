namespace Neomaster.PixelEarth.App;

public class Button(int id)
  : UIElement
{
  public override int Id => id;
  public float Width { get; set; }
  public float Height { get; set; }
  public Action Action { get; set; }
  public ButtonOptions Options { get; set; }
}
