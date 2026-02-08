using Neomaster.PixelEarth.Domain;

namespace Neomaster.PixelEarth.Presentation;

public static class Extensions
{
  public static bool HasFlag(this GameState state, GameStateFlag flag)
  {
    return ((GameStateFlag)state.Flags).HasFlag(flag);
  }

  public static GameState AddFlag(this GameState state, GameStateFlag flag)
  {
    state.Flags |= (int)flag;
    return state;
  }

  public static GameState RemoveFlag(this GameState state, GameStateFlag flag)
  {
    state.Flags &= ~(int)flag;
    return state;
  }
}
