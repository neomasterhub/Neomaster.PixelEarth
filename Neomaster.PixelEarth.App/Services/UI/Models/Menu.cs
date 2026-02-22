using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.App;

public class Menu : MenuBase<MenuItem>
{
  public MenuItem LMBClickedItem { get; set; }
  public List<MenuItem> Items { get; set; }
  public MenuOptions Options { get; set; }
  public override int ItemCount => Items.Count;
  public override MenuItem this[int index] => Items[index];
}
