using Neomaster.PixelEarth.Presentation;

namespace Neomaster.PixelEarth.Infra;

public class UIService(
  IIdGenerator<int> idGenerator,
  IShapeService shapeService)
  : IUIService
{
  private int _selectedId;
  private FrameInfo _frameInfo = new();

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

  public void BeginFrame()
  {
    _frameInfo.Reset();
  }
}
