namespace Neomaster.PixelEarth.App;

public class Grid<TCell>(int id)
  : UIElement
  where TCell : UIElement
{
  public override int Id => id;
  public float Width { get; set; }
  public float Height { get; set; }
  public float CellWidth { get; set; }
  public float CellHeight { get; set; }
  public float Gap { get; set; }
  public Align VerticalAlign { get; set; }
  public Align HorizontalAlign { get; set; }
  public List<TCell> Cells { get; set; } = [];
}
