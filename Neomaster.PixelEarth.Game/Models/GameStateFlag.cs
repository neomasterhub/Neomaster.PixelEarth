namespace Neomaster.PixelEarth.Game;

[Flags]
public enum GameStateFlag
{
  None = 0,
  Loading = 1,
  Paused = 1 << 1,
}
