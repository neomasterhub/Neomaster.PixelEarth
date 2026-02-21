using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Presentation;

public class DemosMenuGameStage : BaseGameStage
{
  private readonly GamePipeline _gamePipeline;

  public DemosMenuGameStage(
    GamePipeline gamePipeline,
    IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
    _gamePipeline = gamePipeline;
  }

  protected override bool RequiresStart()
  {
    return _gamePipeline.HasGameStateFlag(GameStateFlag.ShowDemosMenu);
  }

  protected override void OnUpdate(UpdateEventArgs? e = null)
  {
    throw new NotImplementedException();
  }
}
