using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Domain;

namespace Neomaster.PixelEarth.Presentation;

public static class Extensions
{
  public static Texture Get(this Textures textures, TextureGroupId groupId, TextureId textureId)
  {
    return textures.Get(groupId)[(int)textureId];
  }

  public static TextureGroup Get(this Textures textures, TextureGroupId groupId)
  {
    return textures[(int)groupId];
  }

  public static TGameStageBuffer GetGameStageBuffer<TGameStageBuffer>(this GamePipeline pipeline, GameStageBufferId bufferId)
    where TGameStageBuffer : BaseGameStageBuffer
  {
    return (TGameStageBuffer)pipeline.GetGameStageBuffer((int)bufferId);
  }

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
