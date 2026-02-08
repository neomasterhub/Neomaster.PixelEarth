using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Presentation;

public sealed class MainMenuGameStage : BaseGameStage
{
  private readonly IMainMenuService _mainMenuService;

  public MainMenuGameStage(IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
    _mainMenuService = serviceProvider.GetRequiredService<IMainMenuService>();
  }

  protected override bool RequiresStart()
  {
    throw new NotImplementedException();
  }

  protected override void OnRender(RenderEventArgs? e = null)
  {
    _mainMenuService.Draw();
  }

  protected override void OnUpdate(UpdateEventArgs? e = null)
  {
    throw new NotImplementedException();
  }
}
