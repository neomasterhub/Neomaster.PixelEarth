using Neomaster.PixelEarth.Domain;
using Neomaster.PixelEarth.Presentation;

namespace Neomaster.PixelEarth.Infra;

public class UIService(
  IIdGenerator<int> idGenerator)
  : IUIService
{
  public Button CreateButton(
    float x,
    float y,
    float width,
    float height)
  {
    var button = new Button(idGenerator.Next())
    {
      X = x,
      Y = y,
      Width = width,
      Height = height,
      FillNormal = PresentationConsts.Colors.Red, // TODO: Improve.
      FillHovered = PresentationConsts.Colors.Green, // TODO: Improve.
    };

    return button;
  }
}
