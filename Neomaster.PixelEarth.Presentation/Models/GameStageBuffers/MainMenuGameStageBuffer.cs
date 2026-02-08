using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Presentation;

public sealed class MainMenuGameStageBuffer()
  : BaseGameStageBuffer(GameStageBufferId.MainMenu)
{
  public TextureButton PlayButton { get; set; }
  public TextureButton ExitButton { get; set; }
}
