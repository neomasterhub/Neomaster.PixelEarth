using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Domain;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using static Neomaster.PixelEarth.App.AppConsts;

namespace Neomaster.PixelEarth.Infra;

public class GameWindowService : IGameWindowService
{
  private readonly Textures _textures;
  private readonly GameState _gameState;
  private readonly GameWindow _gameWindow;
  private readonly WindowSettings _windowSettings;
  private readonly IMainMenuService _mainMenuService;
  private readonly IMouseService _mouseService;
  private readonly IShaderService _shaderService;
  private readonly IShapeService _shapeService;
  private readonly ITextureService _textureService;
  private readonly IFrameService _frameService;
  private readonly IUIService _uiService;

  // TODO: remove after full implementation
  private static TextureButton _textureButton1;
  private static TextureButton _textureButton2;

  public GameWindowService(
    Textures textures,
    GameState gameState,
    WindowSettings windowSettings,
    IMainMenuService mainMenuService,
    IMouseService mouseService,
    IShaderService shaderService,
    IShapeService shapeService,
    ITextureService textureService,
    IFrameService frameService,
    IUIService uiService)
  {
    _textures = textures;
    _gameState = gameState;
    _windowSettings = windowSettings;
    _mainMenuService = mainMenuService;
    _mouseService = mouseService;
    _shaderService = shaderService;
    _shapeService = shapeService;
    _textureService = textureService;
    _frameService = frameService;
    _uiService = uiService;

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
    _frameService.ResetFrame();

    _textureService.Load(_textures[TextureGroupName.Test]);

    _mainMenuService.Create(
      [
        new MainMenuItemDef(() => _gameState.FrameState = FrameState.Playing),
        new MainMenuItemDef(() => { }),
        new MainMenuItemDef(() => _gameWindow.Close()),
      ]);

    _textureButton1 = _uiService.CreateTextureButton(100f, 25f, 500f, 500f);
    _textureButton2 = _uiService.CreateTextureButton(190f, 70f, 500f, 500f);
  }

  public void OnRender(RenderEventArgs e)
  {
    _frameService.BeginFrame();

    GL.ClearColor(_windowSettings.BackgroundColor.ToColor4());
    GL.Clear(ClearBufferMask.ColorBufferBit);

    RenderMenu();

    UpdateMouseState(_gameWindow.MouseState.ToMouseStateEventArgs());

    // TODO: remove after full implementation
    _textureService.SetBlending(Blending.Alpha);
    var testTexture = _textures[TextureGroupName.Test];
    _uiService.DrawTextureButton(_textureButton1);
    _uiService.DrawTextureButton(_textureButton2);
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
