using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Domain;

namespace Neomaster.PixelEarth.Presentation;

public static class Extensions
{
  public static bool HasGameStateFlag(this GamePipeline pipeline, GameStateFlag flag)
  {
    return pipeline.GameState.HasFlag(flag);
  }

  public static void AddGameStateFlag(this GamePipeline pipeline, GameStateFlag flag)
  {
    pipeline.GameState.Flags |= (int)flag;
  }

  public static void RemoveGameStateFlag(this GamePipeline pipeline, GameStateFlag flag)
  {
    pipeline.GameState.Flags &= ~(int)flag;
  }

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
