namespace Neomaster.PixelEarth;

public class GameStage
{
  public GameStage()
  {
    OnWindowRender = () => { };
    OnWindowUpdate = () => { };
    NextCondition = () => false;
  }

  public Action OnWindowRender { get; set; }
  public Action OnWindowUpdate { get; set; }
  public Func<bool> NextCondition { get; set; }
}
