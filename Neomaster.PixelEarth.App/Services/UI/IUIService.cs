namespace Neomaster.PixelEarth.App;

public interface IUIService
{
  void DrawMainMenu(
    MainMenu mainMenu);

  MainMenu CreateMainMenu(
    MainMenuButton[] buttons,
    MainMenuOptions? options = null);

  void DrawTextureButton(
    TextureButton button,
    TextureShapeOptions? shapeOptions = null);

  TextureButton CreateTextureButton(
    float x,
    float y,
    float width,
    float height,
    TextureButtonOptions? options = null);

  void DrawColorButton(
    Button button,
    ColorShapeOptions? shapeOptions = null);

  ColorButton CreateColorButton(
    float x,
    float y,
    float width,
    float height,
    ColorButtonOptions? options = null);

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

  void UpdateHoveredIds(UIElement element);
}
