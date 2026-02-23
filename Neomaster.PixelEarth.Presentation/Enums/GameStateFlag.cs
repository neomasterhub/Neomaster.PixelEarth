namespace Neomaster.PixelEarth.Presentation;

[Flags]
public enum GameStateFlag
{
  None = 1,
  Loading = 1 << 1,
  ShowMainMenu = 1 << 2,
  ShowDemosMenu = 1 << 3,
}
