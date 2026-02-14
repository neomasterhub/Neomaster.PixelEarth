using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Presentation;

public sealed class MainMenuGameStage : BaseGameStage
{
  private readonly GamePipeline _gamePipeline;
  private readonly IMainMenuService _mainMenuService;
  private readonly ITextureService _textureService;

  public MainMenuGameStage(
    GamePipeline gamePipeline,
    IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
    _gamePipeline = gamePipeline;
    _mainMenuService = serviceProvider.GetRequiredService<IMainMenuService>();
    _textureService = serviceProvider.GetRequiredService<ITextureService>();
  }

  protected override bool RequiresStart()
  {
    return _gamePipeline.HasGameStateFlag(GameStateFlag.ShowMainMenu);
  }

  protected override void OnRender(RenderEventArgs? e = null)
  {
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
