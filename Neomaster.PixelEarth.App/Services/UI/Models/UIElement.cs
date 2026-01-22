namespace Neomaster.PixelEarth.Presentation;

public abstract class UIElement
{
  public abstract int Id { get; }
  public float X { get; set; }
  public float Y { get; set; }
  public bool MouseLeftPressed { get; set; }
  public bool MouseHoverCaptured { get; set; }
  public bool IsHovered { get; set; }
  public bool IsSelected { get; set; }
}
