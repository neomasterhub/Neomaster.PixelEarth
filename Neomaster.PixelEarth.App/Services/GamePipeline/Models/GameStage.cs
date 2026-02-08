namespace Neomaster.PixelEarth.App;

public abstract class GameStage
{
  public GameStage()
  {
    NextCondition = () => false;
    OnWindowRender = e => { };
    OnWindowUpdate = e => { };
  }

  public Func<bool> NextCondition { get; set; }
  public Action<RenderEventArgs?> OnWindowRender { get; set; }
  public Action<UpdateEventArgs?> OnWindowUpdate { get; set; }
}
