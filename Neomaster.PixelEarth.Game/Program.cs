using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.Infra;
using Neomaster.PixelEarth.Presentation;

new ServiceCollection()
  .AddSingleton(PresentationConsts.WindowSettings)
  .AddSingleton<IGameWindowService, GameWindowService>()
  .BuildServiceProvider()
  .GetRequiredService<IGameWindowService>()
  .Run();
