using Neomaster.PixelEarth.Domain;
using Neomaster.PixelEarth.Presentation;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Neomaster.PixelEarth.Infra;

public class GameWindowService : IGameWindowService
{
  private readonly GameState _gameState;
  private readonly GameWindow _gameWindow;
  private readonly IMainMenuService _mainMenuService;
  private readonly IMouseService _mouseService;
  private readonly IShaderService _shaderService;
  private readonly IShapeService _shapeService;
  private readonly ITextureService _textureService;

  public GameWindowService(
    GameState gameState,
    WindowSettings windowSettings,
    IMainMenuService mainMenuService,
    IMouseService mouseService,
    IShaderService shaderService,
    IShapeService shapeService,
    ITextureService textureService)
  {
    _gameState = gameState;
    _mainMenuService = mainMenuService;
    _mouseService = mouseService;
    _shaderService = shaderService;
    _shapeService = shapeService;
    _textureService = textureService;

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

  public void OnLoad()
  {
    _shaderService.InitializeShaders();
    _shapeService.InitializeBuffers();
    _textureService.Initialize();

    _mainMenuService.Create(
      [
        new MainMenuItemDef(() => _gameState.FrameState = FrameState.Playing),
        new MainMenuItemDef(() => { }),
        new MainMenuItemDef(() => _gameWindow.Close()),
      ]);

    _textureService.Load("test_512x512.png"); // TODO: load specific group
  }

  public void OnRender(RenderEventArgs e)
  {
    GL.ClearColor(PresentationConsts.Color.Background.ToColor4());
    GL.Clear(ClearBufferMask.ColorBufferBit);

    RenderMenu();

    UpdateMouseState(_gameWindow.MouseState.ToMouseStateEventArgs());

    // TODO: remove after full implementation
    _textureService.SetBlending(Blending.Alpha);
    _shapeService.DrawTextureTriangle(
      new(0, 0),
      new(0, 300),
      new(300, 300),
      new(0, 1),
      new(0, 0),
      new(1, 0));
    _textureService.SetBlending(Blending.Replace);

    _gameWindow.SwapBuffers();
  }

  public void OnUpdate(UpdateEventArgs e)
  {
    UpdateMenu();
    UpdatePlaying();
  }

  public void OnExit(ExitEventArgs e)
  {
    _gameState.FrameState = FrameState.Exiting;
  }

  public void UpdateMouseState(MouseStateEventArgs e)
  {
    _mouseService.UpdateMouseState(e);
  }

  private void RenderMenu()
  {
    if (_gameState.FrameState != FrameState.MainMenu)
    {
      return;
    }

    _mainMenuService.Draw();
  }

  private void UpdateMenu()
  {
    if (_gameState.FrameState != FrameState.MainMenu)
    {
      return;
    }

    var keyboard = _gameWindow.KeyboardState;

    if (keyboard.IsKeyPressed(Keys.Down))
    {
      _mainMenuService.MoveDown();
    }
    else if (keyboard.IsKeyPressed(Keys.Up))
    {
      _mainMenuService.MoveUp();
    }
    else if (keyboard.IsKeyPressed(Keys.Enter))
    {
      _mainMenuService.ExecuteSelected();
    }
  }

  private void UpdatePlaying()
  {
    if (_gameState.FrameState != FrameState.Playing)
    {
      return;
    }

    var keyboard = _gameWindow.KeyboardState;

    if (keyboard.IsKeyPressed(Keys.Escape))
    {
      _gameState.FrameState = FrameState.MainMenu;
    }
  }
}
