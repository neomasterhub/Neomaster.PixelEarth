namespace Neomaster.PixelEarth.Presentation;

public readonly struct MouseState
{
  public readonly float X;
  public readonly float Y;

  public MouseState(float x, float y)
  {
    X = x;
    Y = y;
  }
}
