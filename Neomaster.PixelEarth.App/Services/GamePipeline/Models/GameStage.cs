using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth;

public class GameStage
{
  public GameStage()
  {
    Condition = () => false;
    OnWindowRender = e => { };
    OnWindowUpdate = e => { };
  }

  public int Id { get; set; }
  public string Name { get; set; }
  public Func<bool> Condition { get; set; }
  public Action<RenderEventArgs?> OnWindowRender { get; set; }
  public Action<UpdateEventArgs?> OnWindowUpdate { get; set; }
}
