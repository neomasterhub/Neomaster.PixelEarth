namespace Neomaster.PixelEarth.App;

public abstract class GameStage
{
  public GameStage()
  {
    RequiresStart = () => false;
    OnWindowRender = e => { };
    OnWindowUpdate = e => { };
  }

  public Func<bool> RequiresStart { get; set; }
  public Action<RenderEventArgs?> OnWindowRender { get; set; }
  public Action<UpdateEventArgs?> OnWindowUpdate { get; set; }
}
