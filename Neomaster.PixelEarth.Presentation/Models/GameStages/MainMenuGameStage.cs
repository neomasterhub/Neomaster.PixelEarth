using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Presentation;

public sealed class MainMenuGameStage : BaseGameStage
{
  private readonly GamePipeline _gamePipeline;
  private readonly IMainMenuService _mainMenuService;

  public MainMenuGameStage(
    GamePipeline gamePipeline,
    IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
    _gamePipeline = gamePipeline;
    _mainMenuService = serviceProvider.GetRequiredService<IMainMenuService>();
  }

  protected override bool RequiresStart()
  {
    return _gamePipeline.HasGameStateFlag(GameStateFlag.ShowMainMenu);
  }

  protected override void OnRender(RenderEventArgs? e = null)
  {
    _mainMenuService.Draw();
  }

  protected override void OnUpdate(UpdateEventArgs? e = null)
  {
    // TODO: implement keyboard map
  }
}
