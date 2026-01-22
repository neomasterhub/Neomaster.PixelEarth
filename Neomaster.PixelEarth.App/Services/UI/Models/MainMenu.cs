using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.App;

public class MainMenu : Menu<MainMenuButton>
{
  public List<MainMenuButton> Buttons { get; set; }
  public MainMenuOptions Options { get; set; }
  public override int ItemCount => Buttons.Count;
  public override MainMenuButton this[int index] => Buttons[index];
}
