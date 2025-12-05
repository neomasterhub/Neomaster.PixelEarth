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
