using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.Domain;
using Neomaster.PixelEarth.Infra;
using Neomaster.PixelEarth.Presentation;

new ServiceCollection()
  .AddSingleton(PresentationConsts.RenderSettings)
  .AddSingleton(PresentationConsts.WindowSettings)
  .AddSingleton<IIdGenerator<int>, IntIdGenerator>()
  .AddSingleton<IGameWindowService, GameWindowService>()
  .AddSingleton<IMainMenuService, MainMenuService>()
  .AddSingleton<IMouseService, MouseService>()
  .AddSingleton<IShaderService, ShaderService>()
  .AddSingleton<IShapeService, ShapeService>()
  .AddSingleton<IUIService, UIService>()
  .AddSingleton<IFrameService, FrameService>()
  .AddSingleton(new GameState())
  .BuildServiceProvider()
  .GetRequiredService<IGameWindowService>()
  .Run();
