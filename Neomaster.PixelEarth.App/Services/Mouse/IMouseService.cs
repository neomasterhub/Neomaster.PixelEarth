using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.App;

public interface IMouseService
{
  MouseState MouseState { get; }
  void UpdateMouseState(MouseStateEventArgs e);
  AreaMouseState GetMouseState(Rectangle rectangle);
}
