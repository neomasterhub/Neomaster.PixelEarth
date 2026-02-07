using Microsoft.Extensions.DependencyInjection;

namespace Neomaster.PixelEarth.App;

public interface IGameEngineBuilder
{
  IGameEngineBuilder RegisterServices(Action<IServiceCollection> services);
  IGameEngineBuilder AddStage(Func<GamePipeline, GameStage> stageFactory);
  IGameEngineBuilder AddStage(GameStage stage);
  IGameEngine Build();
}
