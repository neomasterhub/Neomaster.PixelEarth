namespace Neomaster.PixelEarth.App;

public record MainMenuItemDef(
  Action action,
  ButtonOptions? buttonOptions = null)
{
  public Action Action => action;
  public ButtonOptions? ButtonOptions => buttonOptions;
}
