namespace Neomaster.PixelEarth.Presentation;

public class Button(int id)
  : UIElement
{
  public override int Id => id;
  public float Width { get; set; }
  public float Height { get; set; }
  public ButtonOptions Options { get; set; }
}
