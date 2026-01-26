using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Domain;
using Neomaster.PixelEarth.Infra;
using Neomaster.PixelEarth.Presentation;
using static Neomaster.PixelEarth.App.AppConsts;

new ServiceCollection()
  .AddSingleton(new GameState())
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
    .AddGroup(new TextureGroup(TextureGroupName.MainMenu)
      .AddTexture(new(TextureName.Test512x512, "test_512x512.png"))
      .AddTexture(new(TextureName.Test512x512Hovered, "test_512x512_H.png"))
      .AddTexture(new(TextureName.Test512x512Selected, "test_512x512_S.png"))
      .AddTexture(new(TextureName.Test512x512SelectedHovered, "test_512x512_SH.png")))
    .AddGroup(new TextureGroup(TextureGroupName.Level1)
      .AddTexture(new(TextureName.Ground, "ground.png"))))
  .BuildServiceProvider()
  .GetRequiredService<IGameWindowService>()
  .Run();
