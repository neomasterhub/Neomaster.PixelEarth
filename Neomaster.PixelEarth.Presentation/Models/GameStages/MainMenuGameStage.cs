using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Presentation;

public sealed class MainMenuGameStage : BaseGameStage
{
  private readonly GamePipeline _gamePipeline;
  private readonly TextureButton _buttonExit;
  private readonly TextureButton _buttonPlay;
  private readonly IMainMenuService _mainMenuService;
  private readonly ITextureService _textureService;
  private readonly IUIService _uiService;

  public MainMenuGameStage(
    GamePipeline gamePipeline,
    TextureButton buttonPlay,
    TextureButton buttonExit,
    IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
    _gamePipeline = gamePipeline;
    _buttonPlay = buttonPlay;
    _buttonExit = buttonExit;
    _mainMenuService = serviceProvider.GetRequiredService<IMainMenuService>();
    _textureService = serviceProvider.GetRequiredService<ITextureService>();
    _uiService = serviceProvider.GetRequiredService<IUIService>();
  }

  protected override bool RequiresStart()
  {
    return _gamePipeline.HasGameStateFlag(GameStateFlag.ShowMainMenu);
  }

  protected override void OnRender(RenderEventArgs? e = null)
  {
    _mainMenuService.Draw();

    // TODO: implement keyboard map
    _textureService.SetBlending(Blending.Alpha);
    _uiService.DrawTextureButton(_buttonPlay);
    _uiService.DrawTextureButton(_buttonExit);
    _textureService.SetBlending(Blending.Replace);
  }

  protected override void OnUpdate(UpdateEventArgs? e = null)
  {
    // TODO: implement keyboard map
  }
}
