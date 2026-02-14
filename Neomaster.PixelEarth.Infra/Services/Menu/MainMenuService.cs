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
    _mainMenu.Items.ForEach(x => x.DrawButton());
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

  [Conditional("DEBUG")]
  private void ThrowIfNotInitialized()
  {
    if (_mainMenu == null)
    {
      throw new InvalidOperationException("The main menu has not been initialized.");
    }
  }
}
