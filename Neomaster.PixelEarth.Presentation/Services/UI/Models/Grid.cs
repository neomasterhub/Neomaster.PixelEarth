namespace Neomaster.PixelEarth.Presentation;

public class Grid<TCell>(int id)
  : UIElement
  where TCell : UIElement
{
  public override int Id => id;
  public float CellWidth { get; set; }
  public float CellHeight { get; set; }
  public float Gap { get; set; }
  public List<TCell> Cells { get; set; } = [];
}
