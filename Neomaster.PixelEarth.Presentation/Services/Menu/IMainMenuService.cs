namespace Neomaster.PixelEarth.Presentation;

public interface IMainMenuService
{
  void Create(
    MainMenuItemDef[] items,
    MainMenuOptions? menuOptions = null,
    ButtonOptions? sharedButtonOptions = null);

  void MoveUp();

  void MoveDown();
}
