using Neomaster.PixelEarth.Presentation;

namespace Neomaster.PixelEarth.Infra;

public class UIService(
  WindowSettings windowSettings,
  IIdGenerator<int> idGenerator,
  IShapeService shapeService,
  IFrameService frameService)
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
    Console.WriteLine($"{_button1.MouseHoverCaptured} {_button2.MouseHoverCaptured} / {_button1.IsHovered} {_button2.IsHovered}");
  }

  public void DrawButton(
    Button button,
    ShapeOptions? shapeOptions = null)
  {
    shapeOptions ??= PresentationConsts.Shape.DefaultOptions;

    button.IsSelected = button.Id == _selectedId;
    button.IsHovered = frameService.FrameInfo.HoveredIds.Contains(button.Id);

    var shapeFillNormal = button.IsSelected
      ? button.Options.FillNormal
      : button.Options.FillSelected;

    var shapeFillHovered = shapeFillNormal;
    if (button.IsHovered)
    {
      shapeFillHovered = button.IsSelected
        ? button.Options.FillHovered
        : button.Options.FillSelectedHovered;
    }

    shapeOptions = shapeOptions.Value
      .FillNormal(shapeFillNormal)
      .FillHovered(shapeFillHovered);

    var shapeState = shapeService.DrawRectangle(
      button.X,
      button.Y,
      button.Width,
      button.Height,
      shapeOptions);

    button.MouseHoverCaptured = shapeState.IsHovered;

    UpdateHoveredIds(button);

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

  public void UpdateHoveredIds(UIElement element)
  {
    if (element.MouseHoverCaptured)
    {
      frameService.FrameInfo.HoveredIds.Add(element.Id);
    }
    else
    {
      frameService.FrameInfo.HoveredIds.Remove(element.Id);
    }

    if (frameService.FrameInfo.HoveredIds.Count > 0)
    {
      frameService.FrameInfo.HoveredIds = [frameService.FrameInfo.HoveredIds.Max()];
    }
  }
}
