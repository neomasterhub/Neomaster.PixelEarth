namespace Neomaster.PixelEarth.Presentation;

public record MainMenuItemDef
{
  public Action Action { get; init; }
  public ButtonOptions? ButtonOptions { get; init; }
}
