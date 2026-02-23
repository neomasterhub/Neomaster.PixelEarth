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
  private readonly ITextureService _textureService;

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
    _textureService = serviceProvider.GetRequiredService<ITextureService>();
  }

  protected override bool RequiresStart()
  {
    return _gamePipeline.HasGameStateFlag(GameStateFlag.ShowDemosMenu);
  }

  protected override void OnRender(RenderEventArgs? e = null)
  {
    _textureService.SetBlending(Blending.Alpha);

    _shapeService.DrawTextureRectangle(
      _mainMenuGameStageBuffer.BgRectangle,
      _mainMenuGameStageBuffer.DemosBgTextureShapeOptions);

    _demosMenuService.Draw();

    _textureService.SetBlending(Blending.Replace);
  }

  protected override void OnUpdate(UpdateEventArgs? e = null)
  {
    if (_gameWindowService.IsAnyKeyUp(ConsoleKey.W, ConsoleKey.UpArrow))
    {
      _demosMenuService.MoveUp();
    }
    else if (_gameWindowService.IsAnyKeyUp(ConsoleKey.S, ConsoleKey.DownArrow))
    {
      _demosMenuService.MoveDown();
    }
    else if (_gameWindowService.IsKeyDown(ConsoleKey.Enter))
    {
      _demosMenuService.ExecuteSelected();
    }
    else if (_gameWindowService.IsKeyDown(ConsoleKey.Escape))
    {
      _demosMenuService.Menu[0].Button.Action();
    }
    else
    {
      _demosMenuService.ExecuteLMBClicked();
    }
  }
}
