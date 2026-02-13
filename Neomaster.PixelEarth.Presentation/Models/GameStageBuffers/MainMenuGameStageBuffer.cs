using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Presentation;

public sealed class MainMenuGameStageBuffer()
  : BaseGameStageBuffer(GameStageBufferId.MainMenu)
{
  public RectangleTextureButton PlayButton { get; set; }
  public RectangleTextureButton ExitButton { get; set; }
}
