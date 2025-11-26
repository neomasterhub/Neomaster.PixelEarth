using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

var nativeSettings = new NativeWindowSettings()
{
  ClientSize = new Vector2i(800, 600),
  Title = "Pixel Earth",
  Flags = ContextFlags.ForwardCompatible,
};

using var window = new GameWindow(GameWindowSettings.Default, nativeSettings);

window.RenderFrame += args =>
{
  GL.ClearColor(0.2f, 0.4f, 0.6f, 1.0f);
  GL.Clear(ClearBufferMask.ColorBufferBit);
  window.SwapBuffers();
};

window.UpdateFrame += args =>
{
  if (window.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape))
  {
    window.Close();
  }
};

window.Run();
