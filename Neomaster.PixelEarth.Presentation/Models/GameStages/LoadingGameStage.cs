using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.App;
using static Neomaster.PixelEarth.App.AppConsts;

namespace Neomaster.PixelEarth.Presentation;

public sealed class LoadingGameStage : BaseGameStage
{
  private readonly Textures _textures;
  private readonly MainMenuGameStageBuffer _mainMenuGameStageBuffer;
  private readonly GamePipeline _gamePipeline;
  private readonly IMainMenuService _mainMenuService;
  private readonly ITextureService _textureService;
  private readonly IUIService _uiService;

  public LoadingGameStage(
    GamePipeline gamePipeline,
    IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
    _gamePipeline = gamePipeline;
    _mainMenuGameStageBuffer = _gamePipeline.GetGameStageBuffer<MainMenuGameStageBuffer>(GameStageBufferId.MainMenu);
    _textures = serviceProvider.GetRequiredService<Textures>();
    _mainMenuService = serviceProvider.GetRequiredService<IMainMenuService>();
    _textureService = serviceProvider.GetRequiredService<ITextureService>();
    _uiService = serviceProvider.GetRequiredService<IUIService>();
  }

  protected override bool RequiresStart()
  {
    return _gamePipeline.HasGameStateFlag(GameStateFlag.Loading);
  }

  protected override void OnUpdate(UpdateEventArgs? e = null)
  {
    _textureService.Load(_textures.Get(TextureGroupId.Test));

    _mainMenuGameStageBuffer.PlayButton = _uiService.CreateTextureButton(100f, 25f, 500f, 500f);
    _mainMenuGameStageBuffer.ExitButton = _uiService.CreateTextureButton(190f, 70f, 500f, 500f);

    _mainMenuService.Create(
      [
        new MainMenuItemDef(() => { }),
        new MainMenuItemDef(() => { }),
      ]);

    _gamePipeline.RemoveGameStateFlag(GameStateFlag.Loading);
    _gamePipeline.AddGameStateFlag(GameStateFlag.ShowMainMenu);
  }
}
