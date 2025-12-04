using Neomaster.PixelEarth.Presentation;
using OpenTK.Windowing.Common;
using GlMouseState = OpenTK.Windowing.GraphicsLibraryFramework.MouseState;

namespace Neomaster.PixelEarth.Infra;

public static class EventArgsExtensions
{
  public static RenderEventArgs ToRenderEventArgs(this FrameEventArgs e)
  {
    return new();
  }

  public static UpdateEventArgs ToUpdateEventArgs(this FrameEventArgs e)
  {
    return new();
  }

  public static MouseStateEventArgs ToMouseStateEventArgs(this GlMouseState e)
  {
    return new MouseStateEventArgs
    {
      MouseState = new(e.X, e.Y),
    };
  }
}
