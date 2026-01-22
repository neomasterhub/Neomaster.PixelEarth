using System.Numerics;

namespace Neomaster.PixelEarth.Presentation;

public interface IMouseService
{
  MouseState MouseState { get; }
  void UpdateMouseState(MouseStateEventArgs e);
  AreaMouseState GetRectangleMouseState(Vector2 topLeft, Vector2 bottomRight);
}
