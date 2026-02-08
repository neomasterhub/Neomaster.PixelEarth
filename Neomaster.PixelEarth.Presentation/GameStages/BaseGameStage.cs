using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Presentation;

public abstract class BaseGameStage : GameStage
{
  protected readonly IServiceProvider _serviceProvider;

  protected BaseGameStage(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
    OnWindowUpdate += OnUpdate;
  }

  protected abstract void OnUpdate(UpdateEventArgs? e = null);
}
