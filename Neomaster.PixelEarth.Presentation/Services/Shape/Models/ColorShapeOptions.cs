using System.Numerics;

namespace Neomaster.PixelEarth.Presentation;

public struct ColorShapeOptions
{
  public CullFaces CullFaces;
  public Vector4 Color;
  public Vector4 ColorHovered;
  public Vector4 ColorSelected;
  public Vector4 ColorSelectedHovered;
  public bool IsHovered;
  public bool IsSelected;

  public readonly Vector4 CurrentColor => IsHovered
    ? (IsSelected ? ColorSelectedHovered : ColorHovered)
    : (IsSelected ? ColorSelected : Color);

  public readonly ShaderProgramArg<Vector4> GetShaderColor(string shaderColorUniformName)
  {
    return new(shaderColorUniformName, CurrentColor);
  }

  public ColorShapeOptions SetHovered(bool isHovered)
  {
    IsHovered = isHovered;
    return this;
  }

  public ColorShapeOptions SetSelected(bool isSelected)
  {
    IsSelected = isSelected;
    return this;
  }
}
