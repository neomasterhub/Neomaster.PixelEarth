using Neomaster.PixelEarth.Presentation;

namespace Neomaster.PixelEarth.Infra;

public class UIService(
  WindowSettings windowSettings,
  IIdGenerator<int> idGenerator,
  IShapeService shapeService)
  : IUIService
{
  private int _selectedId;

  private Button _button1;
  private Button _button2;
  public void DrawMenu()
  {
    var width = 200;
    var height = 200;
    _button1 ??= CreateButton(
      (windowSettings.Width - width) / 2f,
      (windowSettings.Height - height) / 2f,
      width,
      height);
    _button2 ??= CreateButton(
      (windowSettings.Width - width) / 2f + 40f,
      (windowSettings.Height - height) / 2f + 40f,
      width,
      height);
    DrawButton(_button1);
    DrawButton(_button2);
  }

  public void DrawButton(
    Button button,
    ShapeOptions? shapeOptions = null)
  {
    shapeOptions ??= PresentationConsts.Shape.DefaultOptions;

    var isSelected = button.Id == _selectedId;
    var shapeFillNormal = isSelected
      ? button.Options.FillNormal
      : button.Options.FillSelected;
    var shapeFillHovered = isSelected
      ? button.Options.FillHovered
      : button.Options.FillSelectedHovered;

    shapeOptions = shapeOptions.Value
      .FillNormal(shapeFillNormal)
      .FillHovered(shapeFillHovered);

    var shapeState = shapeService.DrawRectangle(
      button.X,
      button.Y,
      button.Width,
      button.Height,
      shapeOptions);

    if (shapeState.IsMouseLeftPressed)
    {
      _selectedId = shapeState.IsHovered
        ? button.Id
        : 0; // TODO: Problem with layers.
    }
  }

  public Button CreateButton(
    float x,
    float y,
    float width,
    float height,
    ButtonOptions? options = null)
  {
    options ??= PresentationConsts.Buttons.DefaultOptions;

    var button = new Button(idGenerator.Next())
    {
      X = x,
      Y = y,
      Width = width,
      Height = height,
      Options = options.Value,
    };

    return button;
  }
}
