namespace Neomaster.PixelEarth.Utils;

public abstract class MenuBase<TItem>
{
  public abstract int ItemCount { get; }
  public int LastIndex => ItemCount - 1;
  public int SelectedIndex { get; set; }
  public TItem SelectedItem => this[SelectedIndex];
  public abstract TItem this[int index] { get; }

  public virtual void MoveDown()
  {
    if (SelectedIndex < LastIndex)
    {
      SelectedIndex++;
    }
    else
    {
      SelectedIndex = 0;
    }
  }

  public virtual void MoveUp()
  {
    if (SelectedIndex > 0)
    {
      SelectedIndex--;
    }
    else
    {
      SelectedIndex = LastIndex;
    }
  }
}
