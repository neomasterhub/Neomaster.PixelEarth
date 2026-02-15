using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.Presentation;

public sealed class MainMenuGameStageBuffer()
  : BaseGameStageBuffer(GameStageBufferId.MainMenu)
{
  public Rectangle BgRectangle { get; set; }
  public TextureShapeOptions BgTextureShapeOptions { get; set; }
}
