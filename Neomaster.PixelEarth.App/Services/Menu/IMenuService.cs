namespace Neomaster.PixelEarth.App;

public interface IMenuService
{
  void Initialize(Menu menu);
  void Draw();
  void MoveUp();
  void MoveDown();
  void ExecuteSelected();
  void ExecuteLMBClicked();
}
