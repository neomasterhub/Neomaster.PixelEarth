using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Presentation;

public abstract class BaseGameStageBuffer : GameStageBuffer
{
  protected BaseGameStageBuffer(GameStageBufferId enumId)
  {
    EnumId = enumId;
    Id = (int)enumId;
  }

  public GameStageBufferId EnumId { get; }

  public override int Id { get; }
}
