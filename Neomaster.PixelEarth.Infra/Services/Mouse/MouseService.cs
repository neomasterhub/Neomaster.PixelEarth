using System.Numerics;
using Neomaster.PixelEarth.Presentation;

namespace Neomaster.PixelEarth.Infra;

public class MouseService : IMouseService
{
  public MouseState MouseState { get; internal set; }

  public void UpdateMouseState(MouseStateEventArgs e)
  {
    MouseState = e.MouseState;
  }

  public bool IsInRectangle(Vector2 topLeft, Vector2 bottomRight)
  {
    var xMin = MathF.Min(topLeft.X, bottomRight.X);
    var xMax = MathF.Max(topLeft.X, bottomRight.X);
    var yMin = MathF.Min(topLeft.Y, bottomRight.Y);
    var yMax = MathF.Max(topLeft.Y, bottomRight.Y);

    return MouseState.Position.X >= xMin
      && MouseState.Position.X <= xMax
      && MouseState.Position.Y >= yMin
      && MouseState.Position.Y <= yMax;
  }
}
