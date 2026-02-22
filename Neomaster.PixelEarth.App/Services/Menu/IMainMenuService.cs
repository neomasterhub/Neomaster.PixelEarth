namespace Neomaster.PixelEarth.App;

public interface IMainMenuService
{
  bool HasItemWithLMBClick { get; }
  void Initialize(MainMenu mainMenu);
  void Draw();
  void MoveUp();
  void MoveDown();
  void ExecuteSelected();
}
