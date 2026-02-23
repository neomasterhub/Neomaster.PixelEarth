using System.Diagnostics;
using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Infra;

public abstract class MenuServiceBase(
  IFrameService frameService)
  : IMenuService
{
  public Menu Menu { get; private set; }

  public virtual void Initialize(Menu menu)
  {
    Menu = menu;
  }

  public virtual void Draw()
  {
    ThrowIfNotInitialized();

    Menu.LMBClickedItem = null;

    var items = Menu.Items;
    for (var i = 0; i < items.Count; i++)
    {
      var item = items[i];

      item.DrawButton();

      if (item.Button.IsSelected)
      {
        Menu.SelectedIndex = i;
      }

      if (item.Button.MouseLeftPressed)
      {
        Menu.LMBClickedItem = item;
      }
    }
  }

  public virtual void MoveUp()
  {
    ThrowIfNotInitialized();
    Menu.MoveUp();
    frameService.FrameInfo.SelectedId = Menu.SelectedItem.Button.Id;
  }

  public virtual void MoveDown()
  {
    ThrowIfNotInitialized();
    Menu.MoveDown();
    frameService.FrameInfo.SelectedId = Menu.SelectedItem.Button.Id;
  }

  public virtual void ExecuteSelected()
  {
    ThrowIfNotInitialized();
    Menu.SelectedItem?.Button.Action();
  }

  public virtual void ExecuteLMBClicked()
  {
    ThrowIfNotInitialized();
    Menu.LMBClickedItem?.Button.Action();
  }

  [Conditional("DEBUG")]
  private void ThrowIfNotInitialized()
  {
    if (Menu == null)
    {
      throw new InvalidOperationException("The main menu has not been initialized.");
    }
  }
}
