namespace Neomaster.PixelEarth.Presentation;

public readonly struct AreaMouseState
{
  public readonly bool IsIn;
  public readonly bool Clicked;

  public AreaMouseState(bool isIn, bool clicked)
  {
    IsIn = isIn;
    Clicked = clicked;
  }
}
