using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Infra;

namespace Neomaster.PixelEarth.Presentation;

public sealed class LoadingGameStage : BaseGameStage
{
  private readonly Textures _textures;
  private readonly MainMenuGameStageBuffer _mainMenuGameStageBuffer;
  private readonly GamePipeline _gamePipeline;
  private readonly IMainMenuService _mainMenuService;
  private readonly ITextureService _textureService;
  private readonly IIdGenerator<int> _idGenerator;

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
    _idGenerator = serviceProvider.GetRequiredService<IIdGenerator<int>>();
  }

  protected override bool RequiresStart()
  {
    return _gamePipeline.HasGameStateFlag(GameStateFlag.Loading);
  }

  protected override void OnUpdate(UpdateEventArgs? e = null)
  {
    _textureService.Load(_textures.Get(TextureGroupId.MainMenu));

    var texMap = _textures.Get(TextureGroupId.MainMenu, TextureId.MainMenu);

    _mainMenuGameStageBuffer.PlayButton = RectangleTextureButtonBuilder
      .Create(texMap)
      .Position(100, 100)
      .Size(140, 40)
      .UvPx(0, 0, 70, 20)
      .UvSelectedPx(0, 20, 70, 20)
      .Build(_idGenerator.Next());

    _mainMenuGameStageBuffer.ExitButton = RectangleTextureButtonBuilder
      .Create(texMap)
      .Position(100, 200)
      .Size(140, 40)
      .UvPx(0, 40, 70, 20)
      .UvSelectedPx(0, 60, 70, 20)
      .Build(_idGenerator.Next());

    _mainMenuService.Create(
      [
        new MainMenuItemDef(() => { }),
        new MainMenuItemDef(() => { }),
      ]);

    _gamePipeline.RemoveGameStateFlag(GameStateFlag.Loading);
    _gamePipeline.AddGameStateFlag(GameStateFlag.ShowMainMenu);
  }
}
