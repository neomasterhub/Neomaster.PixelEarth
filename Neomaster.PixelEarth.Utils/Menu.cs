namespace Neomaster.PixelEarth.Utils;

public abstract class Menu<TItem>
{
  public abstract int ItemCount { get; }
  public int SelectedIndex { get; protected set; }
  public TItem SelectedItem => this[SelectedIndex];
  public abstract TItem this[int index] { get; set; }
}
