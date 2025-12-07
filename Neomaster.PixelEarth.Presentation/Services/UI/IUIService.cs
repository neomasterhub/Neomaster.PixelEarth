namespace Neomaster.PixelEarth.Presentation;

public interface IUIService
{
  void DrawMainMenu();

  void CreateMainMenu(
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

  void UpdateHoveredIds(UIElement element);
}
