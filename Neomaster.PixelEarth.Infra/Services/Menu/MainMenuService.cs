using Neomaster.PixelEarth.Presentation;

namespace Neomaster.PixelEarth.Infra;

public class MainMenuService(
  IUIService uiService,
  IIdGenerator<int> idGenerator)
  : IMainMenuService
{
  public void Create(
    MainMenuItemDef[] items,
    MainMenuOptions? menuOptions = null,
    ButtonOptions? sharedButtonOptions = null)
  {
    var buttons = items
      .Select(i => new MainMenuButton(idGenerator.Next())
      {
        Action = i.Action,
        Options = i.ButtonOptions ?? sharedButtonOptions ?? PresentationConsts.Button.DefaultOptions,
      })
      .ToArray();

    uiService.CreateMainMenu(buttons, menuOptions);
  }

  public void MoveUp()
  {
  }

  public void MoveDown()
  {
  }
}
