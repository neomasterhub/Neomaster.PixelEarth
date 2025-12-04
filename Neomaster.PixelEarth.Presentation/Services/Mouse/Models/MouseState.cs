using System.Numerics;

namespace Neomaster.PixelEarth.Presentation;

public readonly struct MouseState
{
  public readonly Vector2 Position;

  public MouseState(float x, float y)
  {
    Position = new Vector2(x, y);
  }
}
