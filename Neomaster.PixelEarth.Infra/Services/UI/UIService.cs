using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.Infra;

public class UIService(
  WindowSettings windowSettings,
  ButtonOptions buttonOptions,
  MainMenuOptions mainMenuOptions,
  ColorShapeOptions colorShapeOptions,
  IIdGenerator<int> idGenerator,
  IShapeService shapeService,
  IFrameService frameService)
  : IUIService
{
  public void DrawMainMenu(MainMenu mainMenu)
  {
    foreach (var button in mainMenu.Buttons)
    {
      DrawButton(button);
    }
  }

  public MainMenu CreateMainMenu(
    MainMenuButton[] buttons,
    MainMenuOptions? options = null)
  {
    options ??= mainMenuOptions;

    var mainMenu = new MainMenu()
    {
      Options = options.Value,
    };

    var grid = CreateGrid(
      buttons,
      windowSettings.Width,
      windowSettings.Height,
      mainMenu.Options.ButtonWidth,
      mainMenu.Options.ButtonHeight,
      mainMenu.Options.ButtonGap,
      mainMenu.Options.VerticalAlign,
      mainMenu.Options.HorizontalAlign);

    foreach (var button in grid.Cells)
    {
      button.Width = grid.CellWidth;
      button.Height = grid.CellHeight;
    }

    mainMenu.Buttons = grid.Cells;

    return mainMenu;
  }

  public void DrawButton(
    Button button,
    ColorShapeOptions? shapeOptions = null)
  {
    shapeOptions ??= colorShapeOptions;
    shapeOptions = shapeOptions.Value.SetSelected(button.IsSelected);

    button.IsHovered = frameService.FrameInfo.HoveredIds.Contains(button.Id);

    var shapeState = shapeService.DrawColorRectangle(
      button.X,
      button.Y,
      button.Width,
      button.Height,
      shapeOptions);

    button.MouseHoverCaptured = shapeState.IsHovered;

    UpdateHoveredIds(button);

    if (shapeState.IsMouseLeftPressed && shapeState.IsHovered)
    {
      button.MouseLeftPressed = true;
    }
  }

  public Button CreateButton(
    float x,
    float y,
    float width,
    float height,
    ButtonOptions? options = null)
  {
    options ??= buttonOptions;

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
    float gap,
    Align verticalAlign = Align.Begin,
    Align horizontalAlign = Align.Begin)
    where TCell : UIElement
  {
    var grid = new Grid<TCell>(idGenerator.Next())
    {
      Width = gridWidth,
      Height = gridHeight,
      CellWidth = cellWidth,
      CellHeight = cellHeight,
      Gap = gap,
      VerticalAlign = verticalAlign,
      HorizontalAlign = horizontalAlign,
    };

    if (cells.Length == 0)
    {
      return grid;
    }

    var c1 = cells[0];

    var rowCount = MathX.FittableCount(grid.Height, grid.CellHeight, grid.Gap);
    var colHeight = MathX.FittableLength(MathF.Min(cells.Length, rowCount), grid.CellHeight, grid.Gap);
    var colCount = MathF.Ceiling(cells.Length / rowCount);
    var rowWidth = (colCount * (grid.CellWidth + grid.Gap)) - grid.Gap;

    c1.X = grid.HorizontalAlign switch
    {
      Align.Begin => grid.X,
      Align.Center => grid.X + ((grid.Width - rowWidth) / 2),
      Align.End => grid.Width - rowWidth,
      _ => throw grid.HorizontalAlign.ArgumentOutOfRangeException(),
    };

    c1.Y = grid.VerticalAlign switch
    {
      Align.Begin => grid.Y,
      Align.Center => grid.Y + ((grid.Height - colHeight) / 2),
      Align.End => grid.Height - colHeight,
      _ => throw grid.VerticalAlign.ArgumentOutOfRangeException(),
    };

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
