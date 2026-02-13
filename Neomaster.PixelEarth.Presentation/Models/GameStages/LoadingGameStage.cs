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
  private readonly IUIService _uiService;
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
    _uiService = serviceProvider.GetRequiredService<IUIService>();
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

    _mainMenuGameStageBuffer.PlayButton = new RectangleTextureButton(_idGenerator.Next())
    {
      X = 100,
      Y = 100,
      Width = 70,
      Height = 20,
      TextureShapeOptions = new(
        texMap,
        new Utils.Rectangle(0, 0, 70, 20),
        uvSelectedPx: new Utils.Rectangle(0, 20, 70, 20)),
    };

    _mainMenuGameStageBuffer.ExitButton = new RectangleTextureButton(_idGenerator.Next())
    {
      X = 100,
      Y = 200,
      Width = 70 * 2,
      Height = 20 * 2,
      TextureShapeOptions = new(
        texMap,
        new Utils.Rectangle(0, 40, 70, 20),
        uvSelectedPx: new Utils.Rectangle(0, 60, 70, 20)),
    };

    _mainMenuService.Create(
      [
        new MainMenuItemDef(() => { }),
        new MainMenuItemDef(() => { }),
      ]);

    _gamePipeline.RemoveGameStateFlag(GameStateFlag.Loading);
    _gamePipeline.AddGameStateFlag(GameStateFlag.ShowMainMenu);
  }
}
