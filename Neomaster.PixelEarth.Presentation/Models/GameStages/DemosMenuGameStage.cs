using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Infra;

namespace Neomaster.PixelEarth.Presentation;

public class DemosMenuGameStage : BaseGameStage
{
  private readonly GamePipeline _gamePipeline;
  private readonly MainMenuGameStageBuffer _mainMenuGameStageBuffer;
  private readonly IShapeService _shapeService;
  private readonly IGameWindowService _gameWindowService;
  private readonly IMenuService _demosMenuService;

  public DemosMenuGameStage(
    GamePipeline gamePipeline,
    IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
    _gamePipeline = gamePipeline;
    _mainMenuGameStageBuffer = _gamePipeline.GetGameStageBuffer<MainMenuGameStageBuffer>(GameStageBufferId.MainMenu);
    _shapeService = serviceProvider.GetRequiredService<IShapeService>();
    _gameWindowService = serviceProvider.GetRequiredService<IGameWindowService>();
    _demosMenuService = serviceProvider.GetRequiredKeyedService<IMenuService>(MenuId.Demos);
  }

  protected override bool RequiresStart()
  {
    return _gamePipeline.HasGameStateFlag(GameStateFlag.ShowDemosMenu);
  }

  protected override void OnRender(RenderEventArgs? e = null)
  {
    _shapeService.DrawTextureRectangle(
      _mainMenuGameStageBuffer.BgRectangle,
      _mainMenuGameStageBuffer.DemosBgTextureShapeOptions);

    _demosMenuService.Draw();
  }

  protected override void OnUpdate(UpdateEventArgs? e = null)
  {
    if (_gameWindowService.IsAnyKeyUp(ConsoleKey.Escape))
    {
      _gamePipeline.RemoveGameStateFlag(GameStateFlag.ShowDemosMenu);
      _gamePipeline.AddGameStateFlag(GameStateFlag.ShowMainMenu);
    }
  }
}
