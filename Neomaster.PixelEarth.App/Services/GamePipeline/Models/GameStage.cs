namespace Neomaster.PixelEarth;

public class GameStage
{
  public GameStage()
  {
    OnWindowRender = () => { };
    OnWindowUpdate = () => { };
  }

  public int Id { get; set; }
  public Action OnWindowRender { get; set; }
  public Action OnWindowUpdate { get; set; }
}
