namespace Neomaster.PixelEarth.Presentation;

public readonly struct ShapeState
{
  public readonly bool IsHovered;
  public readonly bool IsMouseLeftPressed;

  public ShapeState(bool isHovered, bool isMouseLeftPressed)
  {
    IsHovered = isHovered;
    IsMouseLeftPressed = isMouseLeftPressed;
  }
}
