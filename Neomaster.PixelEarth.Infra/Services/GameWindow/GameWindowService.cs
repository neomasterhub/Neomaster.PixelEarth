using Neomaster.PixelEarth.App;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Neomaster.PixelEarth.Infra;

public class GameWindowService : IGameWindowService
{
  private readonly GameWindow _gameWindow;
  private readonly GamePipeline _gamePipeline;
  private readonly WindowSettings _windowSettings;
  private readonly IMouseService _mouseService;
  private readonly IShaderService _shaderService;
  private readonly IShapeService _shapeService;
  private readonly ITextureService _textureService;
  private readonly IFrameService _frameService;

  public GameWindowService(
    GamePipeline gamePipeline,
    WindowSettings windowSettings,
    IMouseService mouseService,
    IShaderService shaderService,
    IShapeService shapeService,
    ITextureService textureService,
    IFrameService frameService)
  {
    _gamePipeline = gamePipeline;
    _windowSettings = windowSettings;
    _mouseService = mouseService;
    _shaderService = shaderService;
    _shapeService = shapeService;
    _textureService = textureService;
    _frameService = frameService;

    _gameWindow = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings
    {
      ClientSize = new Vector2i(windowSettings.Width, windowSettings.Height),
      Title = windowSettings.Title,
      Flags = ContextFlags.ForwardCompatible,
    });

    _gameWindow.Load += OnLoad;
    _gameWindow.Closing += args => OnExit(args.ToExitEventArgs());
    _gameWindow.RenderFrame += args => OnRender(args.ToRenderEventArgs());
    _gameWindow.UpdateFrame += args => OnUpdate(args.ToUpdateEventArgs());
  }

  public void Run()
  {
    _gameWindow.Run();
  }

  public void Exit()
  {
    _gameWindow.Close();
  }

  public void OnLoad()
  {
    _shaderService.InitializeShaders();
    _shapeService.InitializeBuffers();
    _textureService.Initialize();
    _frameService.ResetFrame();
    _gamePipeline.Update();
  }

  public void OnRender(RenderEventArgs e)
  {
    GL.ClearColor(_windowSettings.BackgroundColor.ToColor4());
    GL.Clear(ClearBufferMask.ColorBufferBit);

    _frameService.BeginFrame();
    _gamePipeline.Next();
    _gamePipeline.Render(e);

    UpdateMouseState(_gameWindow.MouseState.ToMouseStateEventArgs());

    _gameWindow.SwapBuffers();
  }

  public void OnUpdate(UpdateEventArgs e)
  {
    _gamePipeline.Update(e);
  }

  public void OnExit(ExitEventArgs e)
  {
  }

  public void UpdateMouseState(MouseStateEventArgs e)
  {
    _mouseService.UpdateMouseState(e);
  }

  public bool IsKeyUp(ConsoleKey key)
  {
    return _gameWindow.IsKeyReleased(key.ToGlKey());
  }

  public bool IsKeyDown(ConsoleKey key)
  {
    return _gameWindow.IsKeyDown(key.ToGlKey());
  }

  public bool IsAnyKeyUp(params ConsoleKey[] keys)
  {
    for (var i = 0; i < keys.Length; i++)
    {
      if (IsKeyUp(keys[i]))
      {
        return true;
      }
    }

    return false;
  }

  public bool IsAnyKeyDown(params ConsoleKey[] keys)
  {
    for (var i = 0; i < keys.Length; i++)
    {
      if (IsKeyDown(keys[i]))
      {
        return true;
      }
    }

    return false;
  }
}
