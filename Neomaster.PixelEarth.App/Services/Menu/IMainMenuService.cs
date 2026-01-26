namespace Neomaster.PixelEarth.App;

public interface IMainMenuService
{
  void Create(
    MainMenuItemDef[] items,
    MainMenuOptions? menuOptions = null);

  void Draw();
  void MoveUp();
  void MoveDown();
  void ExecuteSelected();
}
