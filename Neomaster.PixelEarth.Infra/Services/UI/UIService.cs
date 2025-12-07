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
  private MainMenu _mainMenu;

  private Button _button1;
  private Button _button2;
  public void DrawMainMenu()
  {
    var width = 200;
    var height = 200;

    if (_mainMenu?.Buttons == null)
    {
      return;
    }

    foreach (var button in _mainMenu.Buttons)
    {
      DrawButton(button);
    }
  }

  public void CreateMainMenu(
    MainMenuButton[] buttons,
    MainMenuOptions? options = null)
  {
    options ??= PresentationConsts.MainMenu.DefaultOptions;

    _mainMenu = new MainMenu(idGenerator.Next())
    {
      Options = options.Value,
    };

    if (buttons.Length > 0)
    {
      var b1 = buttons[0];

      // avH = N * (bH + gap) - gap
      // N = (avH + gap) / (bH + gap)
      var avH = windowSettings.Height - (_mainMenu.Options.Padding * 2);
      var colSize = (int)MathF.Floor((avH + _mainMenu.Options.ButtonGap)
        / (b1.Height + _mainMenu.Options.ButtonGap));

      var colH = (Math.Min(colSize, buttons.Length) * (b1.Height + _mainMenu.Options.ButtonGap)) - _mainMenu.Options.ButtonGap;
      b1.Y = ((avH - colH) / 2) + _mainMenu.Options.Padding;

      var s = (int)Math.Ceiling(buttons.Length / (float)colSize);
      var w = s * b1.Width + s * _mainMenu.Options.ButtonGap - _mainMenu.Options.ButtonGap;
      var avW = windowSettings.Width - (_mainMenu.Options.Padding * 2);
      var x = (avW - w) / 2 + _mainMenu.Options.Padding;
      b1.X = x;
      _mainMenu.Buttons = [];
      _mainMenu.Buttons.Add(b1);

      if (buttons.Length == 1)
      {
        return;
      }

      var row = 2;
      for (var i = 1; i < buttons.Length; i++)
      {
        var b = buttons[i];
        b.X = x;
        if (row == 1)
        {
          b.Y = b1.Y;
        }
        else
        {
          b.Y = buttons[i - 1].Y + b1.Height + _mainMenu.Options.ButtonGap;
        }

        if (colSize == row)
        {
          row = 1;
          x += b1.Width + _mainMenu.Options.ButtonGap;
        }
        else
        {
          row++;
        }

        _mainMenu.Buttons.Add(b);
      }
    }
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
    options ??= PresentationConsts.Button.DefaultOptions;

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
