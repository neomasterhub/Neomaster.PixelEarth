namespace Neomaster.PixelEarth.App;

public struct TextureShapeOptions
{
  public int TextureId;
  public int TextureIdHovered;
  public int TextureIdSelected;
  public int TextureIdSelectedHovered;
  public bool IsHovered;
  public bool IsSelected;

  public readonly int CurrentTextureId => IsHovered
    ? (IsSelected ? TextureIdSelectedHovered : TextureIdHovered)
    : (IsSelected ? TextureIdSelected : TextureId);

  public TextureShapeOptions SetHovered(bool isHovered)
  {
    IsHovered = isHovered;
    return this;
  }

  public TextureShapeOptions SetSelected(bool isSelected)
  {
    IsSelected = isSelected;
    return this;
  }
}
