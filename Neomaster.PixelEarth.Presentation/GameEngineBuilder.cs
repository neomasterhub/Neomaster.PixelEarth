using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Infra;
using static Neomaster.PixelEarth.App.AppConsts;

namespace Neomaster.PixelEarth.Presentation;

public class GameEngineBuilder
{
  private readonly GamePipeline _gamePipeline = new();
  private readonly IServiceCollection _services = new ServiceCollection();

  private IServiceProvider _serviceProvider;

  private GameEngineBuilder()
  {
  }

  public static GameEngineBuilder Create()
  {
    return new();
  }

  public GameEngineBuilder AddDefaultServices()
  {
    _services
      .AddSingleton(_gamePipeline)
      .AddSingleton(PresentationConsts.RenderSettings)
      .AddSingleton(PresentationConsts.WindowSettings)
      .AddSingleton(typeof(ColorButtonOptions), PresentationConsts.Button.ColorDefaultOptions)
      .AddSingleton(typeof(MainMenuOptions), PresentationConsts.MainMenu.DefaultOptions)
      .AddSingleton(typeof(ColorShapeOptions), PresentationConsts.Shape.ColorDefaultOptions)
      .AddSingleton(typeof(TextureShapeOptions), PresentationConsts.Shape.TextureDefaultOptions)
      .AddSingleton(typeof(TextureButtonOptions), PresentationConsts.Button.TextureDefaultOptions)
      .AddSingleton<IIdGenerator<int>, IntIdGenerator>()
      .AddSingleton<IGameWindowService, GameWindowService>()
      .AddSingleton<IMainMenuService, MainMenuService>()
      .AddSingleton<IMouseService, MouseService>()
      .AddSingleton<IShaderService, ShaderService>()
      .AddSingleton<IShapeService, ShapeService>()
      .AddSingleton<IUIService, UIService>()
      .AddSingleton<IImageService, ImageService>()
      .AddSingleton<ITextureService, TextureService>()
      .AddSingleton<IFrameService, FrameService>()
      .AddSingleton(new Textures()
        .AddGroup(new TextureGroup(TextureGroupName.Test)
          .AddTexture(new(TextureName.Test512x512, "test_512x512.png"))
          .AddTexture(new(TextureName.Test512x512Hovered, "test_512x512_H.png"))
          .AddTexture(new(TextureName.Test512x512Selected, "test_512x512_S.png"))
          .AddTexture(new(TextureName.Test512x512SelectedHovered, "test_512x512_SH.png")))
        .AddGroup(new TextureGroup(TextureGroupName.Level1)
          .AddTexture(new(TextureName.Ground, "ground.png"))));

    _serviceProvider = _services.BuildServiceProvider();

    var loadingGameStage = new LoadingGameStage(_gamePipeline, _serviceProvider);

    _gamePipeline
      .AddStage(loadingGameStage)
      .AddStage(p => new MainMenuGameStage(p, loadingGameStage.ButtonPlay, loadingGameStage.ButtonExit, _serviceProvider));

    return this;
  }

  public GameEngineBuilder AddServices(Action<IServiceCollection> configure)
  {
    configure(_services);
    return this;
  }

  public GameEngineBuilder AddGameStage(Func<GamePipeline, GameStage> stageFactory)
  {
    _gamePipeline.AddStage(stageFactory);
    return this;
  }

  public GameEngineBuilder AddGameStage(GameStage stage)
  {
    _gamePipeline.AddStage(stage);
    return this;
  }

  public IGameEngine Build()
  {
    if (_gamePipeline.StageCount == 0)
    {
      throw new InvalidOperationException("Cannot build the engine because the pipeline has no stages.");
    }

    return new GameEngine(_serviceProvider.GetRequiredService<IGameWindowService>());
  }
}
