using Neomaster.PixelEarth.Domain;

namespace Neomaster.PixelEarth.App;

public class GamePipeline
{
  private readonly GameState _gameState;
  private readonly HashSet<GameStage> _stages = [];

  private List<GameStage> _currentStages = [];

  public int StageCount => _stages.Count;

  public GameState GameState => _gameState;

  public GamePipeline AddStage(GameStage stage)
  {
    _stages.Add(stage);
    return this;
  }

  public GamePipeline AddStage(Func<GamePipeline, GameStage> stageFactory)
  {
    _stages.Add(stageFactory(this));
    return this;
  }

  public void Next()
  {
    _currentStages = _stages
      .Where(s => s.Condition())
      .ToList();
  }

  public void Render(RenderEventArgs? e = null)
  {
    _currentStages.ForEach(s => s.OnWindowRender(e));
  }

  public void Update(UpdateEventArgs? e = null)
  {
    _currentStages.ForEach(s => s.OnWindowUpdate(e));
  }
}
