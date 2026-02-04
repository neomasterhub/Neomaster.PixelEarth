namespace Neomaster.PixelEarth;

public class GamePipeline
{
  private readonly HashSet<GameStage> _stages = [];

  private GameStage _currentStage;

  public GamePipeline(GameStage firstStage)
  {
    _stages.Add(firstStage);
    _currentStage = firstStage;
  }

  public GameStage CurrentStage => _currentStage;
}
