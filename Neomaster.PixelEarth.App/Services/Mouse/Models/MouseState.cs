using System.Numerics;

namespace Neomaster.PixelEarth.App;

public readonly struct MouseState
{
  public readonly Vector2 Position;
  public readonly bool LeftPressed;

  public MouseState(float x, float y, bool leftPressed)
  {
    Position = new Vector2(x, y);
    LeftPressed = leftPressed;
  }
}
