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

    var playButton = RectangleTextureButtonBuilder
      .Create(texMap)
      .Size(140, 40)
      .UvPx(0, 0, 70, 20)
      .UvSelectedPx(0, 20, 70, 20)
      .Build(_idGenerator.Next());

    var exitButton = RectangleTextureButtonBuilder
      .Create(texMap)
      .Size(140, 40)
      .UvPx(0, 40, 70, 20)
      .UvSelectedPx(0, 60, 70, 20)
      .Build(_idGenerator.Next());

    _mainMenuGameStageBuffer.MainMenu = new MainMenu
    {
      Items =
      [
        new()
        {
          Button = playButton,
          DrawButton = () => _uiService.DrawRectangleTextureButton(playButton),
        },
        new()
        {
          Button = exitButton,
          DrawButton = () => _uiService.DrawRectangleTextureButton(exitButton),
        },
      ],
      Options = new MainMenuOptions
      {
        ButtonGap = 15,
        ButtonWidth = 140,
        ButtonHeight = 40,
        HorizontalAlign = Align.Center,
        VerticalAlign = Align.Center,
      },
    };

    // Align items.
    _uiService.CreateGrid(
      _mainMenuGameStageBuffer.MainMenu.Items.Select(x => x.Button).ToArray(),
      PresentationConsts.WindowSettings.Width,
      PresentationConsts.WindowSettings.Height,
      _mainMenuGameStageBuffer.MainMenu.Options.ButtonWidth,
      _mainMenuGameStageBuffer.MainMenu.Options.ButtonHeight,
      _mainMenuGameStageBuffer.MainMenu.Options.ButtonGap,
      _mainMenuGameStageBuffer.MainMenu.Options.VerticalAlign,
      _mainMenuGameStageBuffer.MainMenu.Options.HorizontalAlign);

    _mainMenuService.Initialize(_mainMenuGameStageBuffer.MainMenu);

    _gamePipeline.RemoveGameStateFlag(GameStateFlag.Loading);
    _gamePipeline.AddGameStateFlag(GameStateFlag.ShowMainMenu);
  }
}
