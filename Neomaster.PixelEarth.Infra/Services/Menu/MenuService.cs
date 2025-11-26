using Neomaster.PixelEarth.Presentation;

namespace Neomaster.PixelEarth.Infra;

public class MenuService : IMenuService
{
  private readonly int _maxIndex;
  private readonly MenuItem[] _items;

  public MenuService(IEnumerable<MenuItem> items = null)
  {
    _items = items == null
      ? items.ToArray()
      : PresentationConsts.MenuItems;

    _maxIndex = _items.Length - 1;
  }

  public int SelectedIndex { get; private set; }
  public MenuItem SelectedItem => _items[SelectedIndex];

  public void MoveDown()
  {
    if (SelectedIndex == _maxIndex)
    {
      SelectedIndex = 0;
    }
    else
    {
      SelectedIndex++;
    }
  }

  public void MoveUp()
  {
    if (SelectedIndex == 0)
    {
      SelectedIndex = _maxIndex;
    }
    else
    {
      SelectedIndex--;
    }
  }
}
