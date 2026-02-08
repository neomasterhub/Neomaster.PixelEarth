using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Presentation;

public abstract class BaseGameStage : GameStage
{
  protected readonly IServiceProvider _serviceProvider;

  protected BaseGameStage(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
    OnWindowUpdate += OnUpdate;
    OnWindowRender += OnRender;
  }

  protected abstract void OnUpdate(UpdateEventArgs? e = null);

  protected virtual void OnRender(RenderEventArgs? e = null)
  {
  }
}
