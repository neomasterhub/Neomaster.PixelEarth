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
  private readonly GameWindow _gameWindow;
  private readonly WindowSettings _windowSettings;
  private readonly IMenuService _menuService;
  private readonly IMouseService _mouseService;
  private readonly IShaderService _shaderService;
  private readonly IShapeService _shapeService;
  private readonly IUIService _uiService;

  private GameState _gameState = GameState.Menu;

  public GameWindowService(
    WindowSettings windowSettings,
    IMenuService menuService,
    IMouseService mouseService,
    IShaderService shaderService,
    IShapeService shapeService,
    IUIService uiService,
    IIdGenerator<int> idGenerator)
  {
    _menuService = menuService;
    _mouseService = mouseService;
    _windowSettings = windowSettings;
    _shaderService = shaderService;
    _shapeService = shapeService;
    _uiService = uiService;

    // TODO: Simplify
    // TODO: Call on loading
    _uiService.CreateMainMenu(Enumerable.Range(1, 9)
      .Select(x => new MainMenuButton(idGenerator.Next())
      {
        Options = PresentationConsts.Button.DefaultOptions,
      }).ToArray());

    _gameWindow = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings
    {
      ClientSize = new Vector2i(windowSettings.Width, windowSettings.Height),
      Title = windowSettings.Title,
      Flags = ContextFlags.ForwardCompatible,
    });

    _gameWindow.Load += OnLoad;
    _gameWindow.RenderFrame += args => OnRender(args.ToRenderEventArgs());
    _gameWindow.UpdateFrame += args => OnUpdate(args.ToUpdateEventArgs());
  }

  public void Run()
  {
    _gameWindow.Run();
  }

  public void OnLoad()
  {
    _shapeService.InitializeBuffers();

    // TODO: Map "enum - id".
    var triangleProgram = _shaderService.CreateShaderProgram("vertex", "fragment");
    PresentationConsts.Shape.DefaultOptions.ShaderProgramId = triangleProgram.Id;
  }

  public void OnRender(RenderEventArgs e)
  {
    GL.ClearColor(PresentationConsts.Color.Background.ToColor4());
    GL.Clear(ClearBufferMask.ColorBufferBit);

    RenderMenu();
    UpdateMouseState(_gameWindow.MouseState.ToMouseStateEventArgs());

    _gameWindow.SwapBuffers();
  }

  public void OnUpdate(UpdateEventArgs e)
  {
    UpdateMenu();
  }

  public void UpdateMouseState(MouseStateEventArgs e)
  {
    _mouseService.UpdateMouseState(e);
  }

  public void RenderMenu()
  {
    if (_gameState != GameState.Menu)
    {
      return;
    }

    _uiService.DrawMainMenu();
  }

  public void UpdateMenu()
  {
    if (_gameState != GameState.Menu)
    {
      return;
    }

    var keyboard = _gameWindow.KeyboardState;
    if (keyboard.IsKeyPressed(Keys.Down))
    {
      _menuService.MoveDown();
    }
    else if (keyboard.IsKeyPressed(Keys.Up))
    {
      _menuService.MoveUp();
    }
    else if (keyboard.IsKeyPressed(Keys.Enter))
    {
      switch (_menuService.SelectedIndex)
      {
        case 0:
          _gameState = GameState.Playing;
          break;
        case 1:
          _gameWindow.Close();
          break;
      }
    }
  }
}
