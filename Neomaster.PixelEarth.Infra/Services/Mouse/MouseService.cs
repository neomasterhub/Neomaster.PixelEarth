using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.Infra;

public class MouseService : IMouseService
{
  public MouseState MouseState { get; internal set; }

  public void UpdateMouseState(MouseStateEventArgs e)
  {
    MouseState = e.MouseState;
  }

  public AreaMouseState GetMouseState(Rectangle rectangle)
  {
    var x2 = rectangle.X + rectangle.Width;
    var y2 = rectangle.Y + rectangle.Height;
    var xMin = MathF.Min(rectangle.X, x2);
    var xMax = MathF.Max(rectangle.X, x2);
    var yMin = MathF.Min(rectangle.Y, y2);
    var yMax = MathF.Max(rectangle.Y, y2);

    var isIn = MouseState.Position.X >= xMin
      && MouseState.Position.X <= xMax
      && MouseState.Position.Y >= yMin
      && MouseState.Position.Y <= yMax;

    return new AreaMouseState(
      isIn,
      MouseState.LeftPressed);
  }
}
