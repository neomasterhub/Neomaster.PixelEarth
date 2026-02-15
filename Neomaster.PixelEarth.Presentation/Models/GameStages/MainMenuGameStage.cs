using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Presentation;

public sealed class MainMenuGameStage : BaseGameStage
{
  private readonly GamePipeline _gamePipeline;
  private readonly MainMenuGameStageBuffer _mainMenuGameStageBuffer;
  private readonly IMainMenuService _mainMenuService;
  private readonly ITextureService _textureService;
  private readonly IShapeService _shapeService;

  public MainMenuGameStage(
    GamePipeline gamePipeline,
    IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
    _gamePipeline = gamePipeline;
    _mainMenuGameStageBuffer = _gamePipeline.GetGameStageBuffer<MainMenuGameStageBuffer>(GameStageBufferId.MainMenu);
    _mainMenuService = serviceProvider.GetRequiredService<IMainMenuService>();
    _textureService = serviceProvider.GetRequiredService<ITextureService>();
    _shapeService = serviceProvider.GetRequiredService<IShapeService>();
  }

  protected override bool RequiresStart()
  {
    return _gamePipeline.HasGameStateFlag(GameStateFlag.ShowMainMenu);
  }

  protected override void OnRender(RenderEventArgs? e = null)
  {
    _shapeService.DrawTextureRectangle(_mainMenuGameStageBuffer.BgRectangle, _mainMenuGameStageBuffer.BgTextureShapeOptions);
    _textureService.SetBlending(Blending.Alpha);
    _mainMenuService.Draw();
    _textureService.SetBlending(Blending.Replace);
  }

  protected override void OnUpdate(UpdateEventArgs? e = null)
  {
    // TODO: implement keyboard map
    _mainMenuService.ExecuteSelected();
  }
}
