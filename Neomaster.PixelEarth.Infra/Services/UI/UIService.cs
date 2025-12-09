using Neomaster.PixelEarth.Presentation;
using Neomaster.PixelEarth.Utils;

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

  public void DrawMainMenu()
  {
    if (_mainMenu == null)
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

    _mainMenu = new MainMenu()
    {
      Options = options.Value,
    };

    var grid = CreateGrid(
      buttons,
      windowSettings.Width,
      windowSettings.Height,
      _mainMenu.Options.ButtonWidth,
      _mainMenu.Options.ButtonHeight,
      _mainMenu.Options.ButtonGap);

    foreach (var button in grid.Cells)
    {
      button.Width = grid.CellWidth;
      button.Height = grid.CellHeight;
    }

    _mainMenu.Buttons = grid.Cells;
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

  public Grid<TCell> CreateGrid<TCell>(
    TCell[] cells,
    float gridWidth,
    float gridHeight,
    float cellWidth,
    float cellHeight,
    float gap)
    where TCell : UIElement
  {
    var grid = new Grid<TCell>(idGenerator.Next())
    {
      Width = gridWidth,
      Height = gridHeight,
      CellWidth = cellWidth,
      CellHeight = cellHeight,
      Gap = gap,
    };

    if (cells.Length == 0)
    {
      return grid;
    }

    var c1 = cells[0];

    var rowCount = MathX.FittableCount(grid.Height, grid.CellHeight, grid.Gap);
    var colHeight = MathX.FittableLength(rowCount, grid.CellHeight, grid.Gap);
    var colCount = MathF.Ceiling(cells.Length / rowCount);
    var rowWidth = (colCount * (grid.CellWidth + grid.Gap)) - grid.Gap;

    c1.X = grid.X + ((grid.Width - rowWidth) / 2);
    c1.Y = grid.Y + ((grid.Height - colHeight) / 2);
    grid.Cells.Add(c1);

    if (cells.Length == 1)
    {
      return grid;
    }

    var row = 2;
    var x = c1.X;
    for (var i = 1; i < cells.Length; i++)
    {
      var c = cells[i];

      c.X = x;

      if (row == 1)
      {
        c.Y = c1.Y;
      }
      else
      {
        c.Y = cells[i - 1].Y + grid.CellHeight + grid.Gap;
      }

      if (rowCount == row)
      {
        row = 1;
        x += grid.CellWidth + grid.Gap;
      }
      else
      {
        row++;
      }

      grid.Cells.Add(c);
    }

    return grid;
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
