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

    SetBgInfo();
    InitializeMainMenu(texMap);
    InitializeDemosMenu(texMap);

    _gamePipeline.RemoveGameStateFlag(GameStateFlag.Loading);
    _gamePipeline.AddGameStateFlag(GameStateFlag.ShowMainMenu);
  }

  private void SetBgInfo()
  {
    _mainMenuGameStageBuffer.BgRectangle = new(
      0, 0, PresentationConsts.WindowSettings.Width, PresentationConsts.WindowSettings.Height);
    _mainMenuGameStageBuffer.BgTextureShapeOptions = new(
      _textures.Get(TextureGroupId.MainMenu, TextureId.MainMenuBg),
      _mainMenuGameStageBuffer.BgRectangle);
    _mainMenuGameStageBuffer.DemosBgTextureShapeOptions = new(
      _textures.Get(TextureGroupId.MainMenu, TextureId.MainMenuDemosBg),
      _mainMenuGameStageBuffer.BgRectangle);
  }

  private void InitializeMainMenu(Texture texMap)
  {
    // Button
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
      .Action(() =>
      {
        _gamePipeline.RemoveGameStateFlag(GameStateFlag.ShowMainMenu);
        _gamePipeline.AddGameStateFlag(GameStateFlag.ShowDemosMenu);
      })
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

    var mainMenu = new MainMenu
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
      mainMenu.Items.Select(x => x.Button).ToArray(),
      PresentationConsts.WindowSettings.Width,
      PresentationConsts.WindowSettings.Height,
      mainMenu.Options.ButtonWidth,
      mainMenu.Options.ButtonHeight,
      mainMenu.Options.ButtonGap,
      mainMenu.Options.VerticalAlign,
      mainMenu.Options.HorizontalAlign);

    _mainMenuService.Initialize(mainMenu);

    // Title
    var tw = 742;
    var th = 88;
    var tx = (PresentationConsts.WindowSettings.Width - tw) / 2;
    _mainMenuGameStageBuffer.TitleRectangle = new(
      tx, tx, tw, th);
    _mainMenuGameStageBuffer.TitleTextureShapeOptions = new(
      texMap,
      new Utils.Rectangle(w, 0, tw, th));
  }

  private void InitializeDemosMenu(Texture texMap)
  {
    // Button
    var x = 215;
    var w = 207;
    var h = 27;
    var i = 0;
    var y = () => i++ * h;

    var backButton = RectangleTextureButtonBuilder
      .Create(texMap)
      .Size(w, h)
      .UvPx(x, y(), w, h)
      .UvHoveredPx(x, y(), w, h)
      .UvSelectedPx(x, y(), w, h)
      .UvSelectedHoveredPx(x, y(), w, h)
      .Action(() =>
      {
        _gamePipeline.RemoveGameStateFlag(GameStateFlag.ShowDemosMenu);
        _gamePipeline.AddGameStateFlag(GameStateFlag.ShowMainMenu);
      })
      .Build(_idGenerator.Next());
  }
}
