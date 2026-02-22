namespace Neomaster.PixelEarth.App;

public interface IMainMenuService
{
  void Initialize(Menu mainMenu);
  void Draw();
  void MoveUp();
  void MoveDown();
  void ExecuteSelected();
  void ExecuteLMBClicked();
}
