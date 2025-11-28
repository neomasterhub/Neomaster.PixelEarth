namespace Neomaster.PixelEarth.Presentation;

public interface IMenuService
{
  int SelectedIndex { get; }
  MenuItem SelectedItem { get; }
  void MoveDown();
  void MoveUp();
}
