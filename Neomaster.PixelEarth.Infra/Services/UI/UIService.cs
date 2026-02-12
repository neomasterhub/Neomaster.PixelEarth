using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.Infra;

public class UIService(
  WindowSettings windowSettings,
  MainMenuOptions mainMenuOptions,
  IIdGenerator<int> idGenerator,
  IShapeService shapeService,
  IFrameService frameService,
  IMouseService mouseService)
  : IUIService
{
  public void DrawMainMenu(MainMenu mainMenu)
  {
    foreach (var button in mainMenu.Buttons)
    {
      // TODO: draw buttons
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

  public void DrawRectangleTextureButton(RectangleTextureButton button)
  {
    button.IsHovered = frameService.FrameInfo.CurrentHoveredId == button.Id;
    button.IsSelected = frameService.FrameInfo.SelectedId == button.Id;

    var shapeOptions = button.TextureShapeOptions
      .SetHovered(button.IsHovered)
      .SetSelected(button.IsSelected);

    shapeService.DrawTextureRectangle(button.Rectangle, shapeOptions);

    var mouseState = mouseService.GetMouseState(button.Rectangle);

    if (mouseState.IsIn)
    {
      frameService.FrameInfo.NextHoveredId = button.Id;
    }

    if (button.IsHovered && mouseState.LeftPressed)
    {
      frameService.FrameInfo.SelectedId = button.Id;
    }
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
}
