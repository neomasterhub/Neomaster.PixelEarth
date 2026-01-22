using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.Domain;
using Neomaster.PixelEarth.Infra;
using Neomaster.PixelEarth.Presentation;
using static Neomaster.PixelEarth.Presentation.PresentationConsts;

new ServiceCollection()
  .AddSingleton(new GameState())
  .AddSingleton(PresentationConsts.RenderSettings)
  .AddSingleton(PresentationConsts.WindowSettings)
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
      .AddTexture(new(TextureName.Test512x512, "test_512x512.png")))
    .AddGroup(new TextureGroup(TextureGroupName.Level1)
      .AddTexture(new(TextureName.Ground, "ground.png"))))
  .BuildServiceProvider()
  .GetRequiredService<IGameWindowService>()
  .Run();
