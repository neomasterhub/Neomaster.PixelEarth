using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Infra;

public class GameEngine(
  IGameWindowService gameWindowService)
  : IGameEngine
{
  public void Run()
  {
    gameWindowService.Run();
  }
}
