using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Presentation;

public sealed class MainMenuGameStageBuffer()
  : BaseGameStageBuffer(GameStageBufferId.MainMenu)
{
  public MainMenu MainMenu { get; set; }
}
