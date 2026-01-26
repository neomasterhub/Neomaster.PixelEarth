using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Infra;

public class MainMenuService(
  IUIService uiService,
  IIdGenerator<int> idGenerator)
  : IMainMenuService
{
  private MainMenu _mainMenu;

  public void Create(
    MainMenuItemDef[] items,
    MainMenuOptions? menuOptions = null)
  {
    var buttons = items
      .Select(i => new MainMenuButton(idGenerator.Next())
      {
        Action = i.Action,
      })
      .ToArray();

    _mainMenu = uiService.CreateMainMenu(buttons, menuOptions);
  }

  public void Draw()
  {
    _mainMenu.SelectedItem.IsSelected = true;

    uiService.DrawMainMenu(_mainMenu);

    _mainMenu.SelectedItem.IsSelected = false;

    UpdateSelectedIndex();
  }

  public void MoveUp()
  {
    _mainMenu.MoveUp();
  }

  public void MoveDown()
  {
    _mainMenu.MoveDown();
  }

  public void ExecuteSelected()
  {
    _mainMenu.SelectedItem.Action?.Invoke();
  }

  private void UpdateSelectedIndex()
  {
    for (var i = 0; i < _mainMenu.ItemCount; i++)
    {
      var b = _mainMenu[i];

      if (b.MouseLeftPressed)
      {
        _mainMenu.SelectedIndex = i;
        b.MouseLeftPressed = false;

        break;
      }
    }
  }
}
