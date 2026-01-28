using System.Numerics;

namespace Neomaster.PixelEarth.App;

public interface IMouseService
{
  MouseState MouseState { get; }
  void UpdateMouseState(MouseStateEventArgs e);
  AreaMouseState GetRectangleMouseState(Vector2 topLeft, Vector2 bottomRight);
  AreaMouseState GetRectangleMouseState(float x, float y, float width, float height);
}
