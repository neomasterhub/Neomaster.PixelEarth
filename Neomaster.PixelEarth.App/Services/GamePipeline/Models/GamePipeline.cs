namespace Neomaster.PixelEarth;

public class GamePipeline
{
  private readonly HashSet<GameStage> _stages = [];

  private int _currentStageId;

  public GamePipeline(GameStage firstStage)
  {
    _stages.Add(firstStage);
    _currentStageId = firstStage.Id;
  }
}
