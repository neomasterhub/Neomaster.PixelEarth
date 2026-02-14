using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.App;

public class MainMenu : Menu<MainMenuItem>
{
  public List<MainMenuItem> Items { get; set; }
  public MainMenuOptions Options { get; set; }
  public override int ItemCount => Items.Count;
  public override MainMenuItem this[int index] => Items[index];
}
