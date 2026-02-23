using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Infra;

namespace Neomaster.PixelEarth.Presentation;

public sealed class MainMenuGameStage : BaseGameStage
{
  private readonly GamePipeline _gamePipeline;
  private readonly MainMenuGameStageBuffer _mainMenuGameStageBuffer;
  private readonly IGameWindowService _gameWindowService;
  private readonly IMenuService _mainMenuService;
  private readonly ITextureService _textureService;
  private readonly IShapeService _shapeService;

  public MainMenuGameStage(
    GamePipeline gamePipeline,
    IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
    _gamePipeline = gamePipeline;
    _mainMenuGameStageBuffer = _gamePipeline.GetGameStageBuffer<MainMenuGameStageBuffer>(GameStageBufferId.MainMenu);
    _gameWindowService = serviceProvider.GetRequiredService<IGameWindowService>();
    _mainMenuService = serviceProvider.GetRequiredKeyedService<IMenuService>(MenuId.Main);
    _textureService = serviceProvider.GetRequiredService<ITextureService>();
    _shapeService = serviceProvider.GetRequiredService<IShapeService>();
  }

  protected override bool RequiresStart()
  {
    return _gamePipeline.HasGameStateFlag(GameStateFlag.ShowMainMenu);
  }

  protected override void OnRender(RenderEventArgs? e = null)
  {
    _textureService.SetBlending(Blending.Alpha);
    _shapeService.DrawTextureRectangle(_mainMenuGameStageBuffer.BgRectangle, _mainMenuGameStageBuffer.BgTextureShapeOptions);
    _shapeService.DrawTextureRectangle(_mainMenuGameStageBuffer.TitleRectangle, _mainMenuGameStageBuffer.TitleTextureShapeOptions);
    _mainMenuService.Draw();
    _textureService.SetBlending(Blending.Replace);
  }

  protected override void OnUpdate(UpdateEventArgs? e = null)
  {
    if (_gameWindowService.IsAnyKeyUp(ConsoleKey.W, ConsoleKey.UpArrow))
    {
      _mainMenuService.MoveUp();
    }
    else if (_gameWindowService.IsAnyKeyUp(ConsoleKey.S, ConsoleKey.DownArrow))
    {
      _mainMenuService.MoveDown();
    }
    else if (_gameWindowService.IsKeyUp(ConsoleKey.Enter))
    {
      _mainMenuService.ExecuteSelected();
    }
    else
    {
      _mainMenuService.ExecuteLMBClicked();
    }
  }
}
