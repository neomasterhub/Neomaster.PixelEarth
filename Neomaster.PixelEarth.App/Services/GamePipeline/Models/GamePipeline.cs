namespace Neomaster.PixelEarth;

public class GamePipeline
{
  private readonly HashSet<GameStage> _stages = [];

  public GamePipeline AddStage(GameStage stage)
  {
    _stages.Add(stage);
    return this;
  }
}
