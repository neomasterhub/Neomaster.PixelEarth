namespace Neomaster.PixelEarth.Presentation;

public interface IUIService
{
  void DrawMainMenu(
    MainMenu mainMenu);

  MainMenu CreateMainMenu(
    MainMenuButton[] buttons,
    MainMenuOptions? options = null);

  void DrawButton(
    Button button,
    ShapeOptions? shapeOptions = null);

  Button CreateButton(
    float x,
    float y,
    float width,
    float height,
    ButtonOptions? options = null);

  Grid<TCell> CreateGrid<TCell>(
    TCell[] cells,
    float gridWidth,
    float gridHeight,
    float cellWidth,
    float cellHeight,
    float gap)
    where TCell : UIElement;

  void UpdateHoveredIds(UIElement element);
}
