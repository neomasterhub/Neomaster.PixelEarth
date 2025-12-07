namespace Neomaster.PixelEarth.Presentation;

public class MainMenu(int id)
  : UIElement
{
  public override int Id => id;
  public List<MainMenuButton> Buttons { get; set; }
}
