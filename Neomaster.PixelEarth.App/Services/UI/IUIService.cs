namespace Neomaster.PixelEarth.App;

public interface IUIService
{
  void DrawRectangleTextureButton(RectangleTextureButton button);

  Grid<TCell> CreateGrid<TCell>(
    TCell[] cells,
    float gridWidth,
    float gridHeight,
    float cellWidth,
    float cellHeight,
    float gap,
    Align verticalAlign = Align.Begin,
    Align horizontalAlign = Align.Begin)
    where TCell : UIElement;
}
