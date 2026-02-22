using System.Diagnostics;
using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Infra;

public class MainMenuService : IMainMenuService
{
  private MainMenu _mainMenu;

  public void Initialize(MainMenu mainMenu)
  {
    _mainMenu = mainMenu;
  }

  public void Draw()
  {
    ThrowIfNotInitialized();

    _mainMenu.LMBClickedItem = null;

    var items = _mainMenu.Items;
    for (var i = 0; i < items.Count; i++)
    {
      var item = items[i];

      item.DrawButton();

      if (item.Button.IsSelected)
      {
        _mainMenu.SelectedIndex = i;
      }

      if (item.Button.MouseLeftPressed)
      {
        _mainMenu.LMBClickedItem = item;
      }
    }
  }

  public void MoveUp()
  {
    ThrowIfNotInitialized();
    _mainMenu.MoveUp();
  }

  public void MoveDown()
  {
    ThrowIfNotInitialized();
    _mainMenu.MoveDown();
  }

  public void ExecuteSelected()
  {
    ThrowIfNotInitialized();
    _mainMenu.SelectedItem?.Button.Action();
  }

  public void ExecuteLMBClicked()
  {
    ThrowIfNotInitialized();
    _mainMenu.LMBClickedItem?.Button.Action();
  }

  [Conditional("DEBUG")]
  private void ThrowIfNotInitialized()
  {
    if (_mainMenu == null)
    {
      throw new InvalidOperationException("The main menu has not been initialized.");
    }
  }
}
