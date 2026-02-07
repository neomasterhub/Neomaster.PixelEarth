using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Infra;

public class GameEngineBuilder(
  IServiceCollection services,
  GamePipeline gamePipeline)
  : IGameEngineBuilder
{
  public IGameEngineBuilder RegisterServices(Action<IServiceCollection> configure)
  {
    configure(services);
    return this;
  }

  public IGameEngineBuilder AddStage(Func<GamePipeline, GameStage> stageFactory)
  {
    gamePipeline.AddStage(stageFactory);
    return this;
  }

  public IGameEngineBuilder AddStage(GameStage stage)
  {
    gamePipeline.AddStage(stage);
    return this;
  }

  public IGameEngine Build()
  {
    return new GameEngine(services
      .BuildServiceProvider()
      .GetRequiredService<IGameWindowService>());
  }
}
