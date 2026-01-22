namespace Neomaster.PixelEarth.Presentation;

public readonly struct AreaMouseState
{
  public readonly bool IsIn;
  public readonly bool LeftPressed;

  public AreaMouseState(bool isIn, bool leftPressed)
  {
    IsIn = isIn;
    LeftPressed = leftPressed;
  }
}
