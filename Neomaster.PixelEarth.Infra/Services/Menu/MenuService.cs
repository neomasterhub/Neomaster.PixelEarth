using System.Diagnostics;
using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Infra;

public class MenuService(
  IFrameService frameService)
  : IMenuService
{
  private Menu _menu;

  public void Initialize(Menu menu)
  {
    _menu = menu;
  }

  public void Draw()
  {
    ThrowIfNotInitialized();

    _menu.LMBClickedItem = null;

    var items = _menu.Items;
    for (var i = 0; i < items.Count; i++)
    {
      var item = items[i];

      item.DrawButton();

      if (item.Button.IsSelected)
      {
        _menu.SelectedIndex = i;
      }

      if (item.Button.MouseLeftPressed)
      {
        _menu.LMBClickedItem = item;
      }
    }
  }

  public void MoveUp()
  {
    ThrowIfNotInitialized();
    _menu.MoveUp();
    frameService.FrameInfo.SelectedId = _menu.SelectedItem.Button.Id;
  }

  public void MoveDown()
  {
    ThrowIfNotInitialized();
    _menu.MoveDown();
    frameService.FrameInfo.SelectedId = _menu.SelectedItem.Button.Id;
  }

  public void ExecuteSelected()
  {
    ThrowIfNotInitialized();
    _menu.SelectedItem?.Button.Action();
  }

  public void ExecuteLMBClicked()
  {
    ThrowIfNotInitialized();
    _menu.LMBClickedItem?.Button.Action();
  }

  [Conditional("DEBUG")]
  private void ThrowIfNotInitialized()
  {
    if (_menu == null)
    {
      throw new InvalidOperationException("The main menu has not been initialized.");
    }
  }
}
