using Neomaster.PixelEarth.Presentation;
using OpenTK.Windowing.Common;

namespace Neomaster.PixelEarth.Infra;

public static class FrameEventArgsExtensions
{
  public static RenderEventArgs ToRenderEventArgs(this FrameEventArgs e)
  {
    return new();
  }

  public static UpdateEventArgs ToUpdateEventArgs(this FrameEventArgs e)
  {
    return new();
  }
}
