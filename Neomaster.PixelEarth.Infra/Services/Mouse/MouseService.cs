using Neomaster.PixelEarth.Presentation;

namespace Neomaster.PixelEarth.Infra;

public class MouseService : IMouseService
{
  public static MouseState MouseState { get; internal set; }
}
