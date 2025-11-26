using Neomaster.PixelEarth.Presentation;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Neomaster.PixelEarth.Infra;

public class GameWindowService : IGameWindowService
{
  private readonly GameWindow _gameWindow;
  private readonly IMenuService _menuService;

  public GameWindowService(
    WindowSettings windowSettings,
    IMenuService menuService)
  {
    _gameWindow = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings
    {
      ClientSize = new Vector2i(windowSettings.Width, windowSettings.Height),
      Title = windowSettings.Title,
      Flags = ContextFlags.ForwardCompatible,
    });

    _gameWindow.RenderFrame += args => OnRender(args.ToRenderEventArgs());
    _gameWindow.UpdateFrame += args => OnUpdate(args.ToUpdateEventArgs());

    _menuService = menuService;
  }

  public void OnRender(RenderEventArgs e)
  {
    GL.ClearColor(PresentationConsts.Colors.Background.ToColor4());
    GL.Clear(ClearBufferMask.ColorBufferBit);
    _gameWindow.SwapBuffers();
  }

  public void OnUpdate(UpdateEventArgs e)
  {
    if (_gameWindow.IsKeyDown(Keys.Escape))
    {
      _gameWindow.Close();
    }
  }

  public void Run()
  {
    _gameWindow.Run();
  }

  public void RenderMenu()
  {
    Console.Clear();
    Console.WriteLine(_menuService.SelectedItem.Text);
  }
}
