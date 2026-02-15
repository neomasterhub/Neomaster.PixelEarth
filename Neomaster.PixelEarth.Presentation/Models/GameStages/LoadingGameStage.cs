using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Infra;

namespace Neomaster.PixelEarth.Presentation;

public sealed class LoadingGameStage : BaseGameStage
{
  private readonly Textures _textures;
  private readonly MainMenuGameStageBuffer _mainMenuGameStageBuffer;
  private readonly GamePipeline _gamePipeline;
  private readonly IGameWindowService _gameWindowService;
  private readonly IMainMenuService _mainMenuService;
  private readonly ITextureService _textureService;
  private readonly IFrameService _frameService;
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
    _gameWindowService = serviceProvider.GetRequiredService<IGameWindowService>();
    _mainMenuService = serviceProvider.GetRequiredService<IMainMenuService>();
    _textureService = serviceProvider.GetRequiredService<ITextureService>();
    _frameService = serviceProvider.GetRequiredService<IFrameService>();
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

    var texMap = _textures.Get(TextureGroupId.MainMenu, TextureId.MainMenuMap);

    // Button:
    var w = 207;
    var h = 50;
    var i = 0;
    var y = () => i++ * h;

    var playButton = RectangleTextureButtonBuilder
      .Create(texMap)
      .Size(w, h)
      .UvPx(0, y(), w, h)
      .UvHoveredPx(0, y(), w, h)
      .UvSelectedPx(0, y(), w, h)
      .UvSelectedHoveredPx(0, y(), w, h)
      .Build(_idGenerator.Next());

    var demosButton = RectangleTextureButtonBuilder
      .Create(texMap)
      .Size(w, h)
      .UvPx(0, y(), w, h)
      .UvHoveredPx(0, y(), w, h)
      .UvSelectedPx(0, y(), w, h)
      .UvSelectedHoveredPx(0, y(), w, h)
      .Build(_idGenerator.Next());

    var exitButton = RectangleTextureButtonBuilder
      .Create(texMap)
      .Size(w, h)
      .UvPx(0, y(), w, h)
      .UvHoveredPx(0, y(), w, h)
      .UvSelectedPx(0, y(), w, h)
      .UvSelectedHoveredPx(0, y(), w, h)
      .Action(_gameWindowService.Exit)
      .Build(_idGenerator.Next());

    _frameService.FrameInfo.SelectedId = playButton.Id;

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
          Button = demosButton,
          DrawButton = () => _uiService.DrawRectangleTextureButton(demosButton),
        },
        new()
        {
          Button = exitButton,
          DrawButton = () => _uiService.DrawRectangleTextureButton(exitButton),
        },
      ],
      Options = new MainMenuOptions
      {
        ButtonGap = 10,
        ButtonWidth = w,
        ButtonHeight = h,
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
