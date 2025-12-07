using System;

namespace Neomaster.PixelEarth.Utils;

public static class MathX
{
  public static float FittableCount(
    float maxLength,
    float elementLength,
    float gap)
  {
    // L = N * (l + g) - g
    // N = floor((L + g) / (l + g))
    return MathF.Floor((maxLength + gap) / (elementLength + gap));
  }

  public static float FittableLength(
    float elementCount,
    float elementLength,
    float gap)
  {
    return (elementCount * (elementLength + gap)) - gap;
  }
}
