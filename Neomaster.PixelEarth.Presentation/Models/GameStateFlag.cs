namespace Neomaster.PixelEarth.Presentation;

[Flags]
public enum GameStateFlag
{
  None = 0,
  Loading = 1,
  ShowMainMenu = 1 << 1,
}
